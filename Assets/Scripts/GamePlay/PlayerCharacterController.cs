
using UnityEngine;
using Unity.TPS.Audio;
using Unity.TPS.Game;
namespace Unity.TPS.Gameplay
{
    public enum PlayState {
        idle,
        jumping,
        crounch,
        prone,
        moving
    }
    public class PlayerCharacterController : MonoBehaviour {
        // Start is called before the first frame update
        public float RunSpeed = 3f;
        public float WalkSpeed = 1f;
        public float CrouchSpeed = 0.5f;
        public float TurnSpeed = 100;
        float m_CameraVerticalAngle = 0f;
        InputHandler m_InputHandler;
        public float RotationSpeed = 200f;
        public float RotationMultiplier {
            get {
                return 1f;
            }
        }
        public Camera PlayerCamera;
        CharacterController m_controller;
        public float JumpForce = 200f;
        public float vertSpeed = 0f;
        public Vector3 MovementSpeed;
        PlayerAnimatorController playerAnimatorController;
        float lastJumpTime;
        public PlayState playState;
        public float targetJumpSpeed = 0f;
        public float fallSharpness = 4f;
        public float fallSpeed = 0.1f;
        public LayerMask GroundCheckLayers = -1;
        public float GroundCheckDistance = 0.05f;
        public bool isGrounded = false;
        public float landHeight = 0.1f;
        public AudioController audioController;

        Vector3 previousPosition;
        public float minimumMoveThreshold;
        Health health;
        void Start()
        {
            health = GetComponent<Health>();
            m_controller = GetComponent<CharacterController>();
            m_InputHandler = GetComponent<InputHandler>();
            playerAnimatorController = GetComponent<PlayerAnimatorController>();
            playState = PlayState.idle;
            previousPosition = transform.position;
            health.onDamage += onDamage;
            health.onDie += onDie;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q)) {
                Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            }
            HandleCharactorMovement();
            HandleRotation();   
        }
        private void FixedUpdate() {
            if (playState == PlayState.jumping && MovementSpeed.y <= 0 && checkHeight(landHeight, out RaycastHit hit)) {
                playerAnimatorController.setLandAni();
                playState = PlayState.idle;
            }
        }
        public float k_GroundCheckDistance = 0.07f;
        public bool CheckGrounded() {
            float chosenGroundCheckDistance = isGrounded ? (m_controller.skinWidth + GroundCheckDistance) : k_GroundCheckDistance;
            if(checkHeight(chosenGroundCheckDistance, out RaycastHit hit)) {
                // if (playState == PlayState.jumping) {
                //     print(hit.collider.name);
                //     Time.timeScale = 0;
                // }
                return hit.collider.name != "ToonSoldiers_gunner";
            }
            return false;
            // return Physics.Raycast(transform.position, -Vector3.up, 0.05f);
        }
        public bool checkHeight(float maxHeight, out RaycastHit hit0) {
            hit0 = new RaycastHit();
            if(Physics.CapsuleCast(GetCapsuleBottomHemisphere(), GetCapsuleTopHemisphere(m_controller.height), m_controller.radius, Vector3.down, out RaycastHit hit, maxHeight, GroundCheckLayers,
            QueryTriggerInteraction.Ignore
            )) {
                hit0 = hit;
                return true;
            }
            return false;
        }
        void HandleCharactorMovement() {
            // m_RigidBody.MovePosition(m_RigidBody.position + movementDir * MoveSpeed * Time.deltaTime);
            float moveSpeed = RunSpeed;
            if (m_InputHandler.isCrouching) moveSpeed = CrouchSpeed;
            if (m_InputHandler.isWalking) moveSpeed = WalkSpeed;
            Vector3 worldspaceMoveInput = transform.TransformVector(m_InputHandler.GetMoveInput());
            Vector3 targetSpeed = worldspaceMoveInput * RunSpeed;
            if (!CheckGrounded()) {
                vertSpeed += -9.8f * fallSpeed * Time.deltaTime;
                if (vertSpeed < -8.0f) {
                    vertSpeed = -8.0f;
                }
                targetJumpSpeed += vertSpeed;
            } else if (MovementSpeed.y <= 0){
                isGrounded = true;
                vertSpeed = 0f;
                targetJumpSpeed = 0f;
                MovementSpeed.y = 0f;
                playState = PlayState.idle;
            }
            // handle jump
            if (playState != PlayState.jumping && m_InputHandler.GetJumpInputDown() && CheckGrounded()) {
                Debug.Log("space");
                isGrounded = false;
                targetJumpSpeed += JumpForce;
                playState = PlayState.jumping;
                lastJumpTime = Time.time;
                playerAnimatorController.setJumpedAni();
                print(targetJumpSpeed);
                MovementSpeed.y += m_controller.skinWidth + k_GroundCheckDistance + 0.1f;
            }
            
            MovementSpeed.y = Mathf.Lerp(MovementSpeed.y, targetJumpSpeed, fallSharpness * Time.deltaTime);
            MovementSpeed.x = Mathf.Lerp(MovementSpeed.x, targetSpeed.x, 10 * Time.deltaTime);
            MovementSpeed.z = Mathf.Lerp(MovementSpeed.z, targetSpeed.z, 10 * Time.deltaTime);
            m_controller.Move(MovementSpeed * Time.deltaTime);

            if (Vector3.Distance(previousPosition, transform.position) >= minimumMoveThreshold)
                audioController.Play();
            else {
                audioController.Stop();
            }

            previousPosition = transform.position;
        }
        void HandleRotation() {
            m_CameraVerticalAngle -= m_InputHandler.GetLookInputsVertical() * RotationSpeed * RotationMultiplier;

            // limit the camera's vertical angle to min/max
            m_CameraVerticalAngle = Mathf.Clamp(m_CameraVerticalAngle, -89f, 89f);

            // apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down)
            PlayerCamera.transform.localEulerAngles = new Vector3(m_CameraVerticalAngle, 0, 0);
            transform.Rotate(new Vector3(0f, m_InputHandler.GetLookInputsHorizontal() * RotationSpeed * RotationMultiplier, 0f), Space.Self);
        }
        public Vector3 GetMovementSpeed() {
            return MovementSpeed;
        }
         Vector3 GetCapsuleBottomHemisphere()
        {
            return transform.position + (transform.up * m_controller.radius);
        }

        // Gets the center point of the top hemisphere of the character controller capsule    
        Vector3 GetCapsuleTopHemisphere(float atHeight)
        {
            return transform.position + (transform.up * (atHeight - m_controller.radius));
        }

        void onDamage() {
            print("take damage");
        }
        void onDie(GameObject GO) {
            print("die");
            playerAnimatorController.SetDie();
            Invoke("DestroySelf", 0f);
        }
        void DestroySelf() {
            Destroy(this.gameObject);
        }
    }
    
}
