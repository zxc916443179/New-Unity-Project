
using UnityEngine;
namespace Unity.TPS.Gameplay
{
    public class PlayerCharacterController : MonoBehaviour {
        // Start is called before the first frame update
        public float MoveSpeed = 3f;
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
        float vertSpeed = 0f;
        public Vector3 MovementSpeed {get; set;}
        void Start()
        {
            m_controller = GetComponent<CharacterController>();
            m_InputHandler = GetComponent<InputHandler>();
        }

        // Update is called once per frame
        void Update()
        {
           HandleCharactorMovement();
           Debug.Log(MovementSpeed.sqrMagnitude);
        }
        Vector3 Grounded(Vector3 targetVelocity) {
            if (!m_controller.isGrounded) {
                vertSpeed += -9.8f * 0.03f * Time.deltaTime;
                if (vertSpeed < -10.0f) {
                    vertSpeed = -10.0f;
                }
                targetVelocity.y += vertSpeed;
            }
            else {
                vertSpeed = 0f;
            }
            return targetVelocity;
        }
        void HandleCharactorMovement() {
            // m_RigidBody.MovePosition(m_RigidBody.position + movementDir * MoveSpeed * Time.deltaTime);
            Vector3 worldspaceMoveInput = transform.TransformVector(m_InputHandler.GetMoveInput());
            Vector3 targetVelocity = worldspaceMoveInput * MoveSpeed;
            // handle jump
            if (m_InputHandler.GetJumpInputDown() && m_controller.isGrounded) {
                Debug.Log("space");
                targetVelocity = new Vector3(targetVelocity.x, 0f, targetVelocity.z);
                targetVelocity += Vector3.up * JumpForce;
            }
            MovementSpeed = Vector3.Lerp(MovementSpeed, targetVelocity, 10 * Time.deltaTime);
            MovementSpeed = Grounded(MovementSpeed);
            m_controller.Move(MovementSpeed * Time.deltaTime);
            HandleRotation();
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
    }
    
}
