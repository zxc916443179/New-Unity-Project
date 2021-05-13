using UnityEngine;
using Unity.TPS.Game;
namespace Unity.TPS.Gameplay {
    public class PlayerAnimatorController : MonoBehaviour {
        Animator animator;
        PlayerCharacterController playerCharacterController;
        public float walkSpeed = 0.1f;
        public float runSpeed = 5f;
        const string k_AnimMoveLeftParameter = "MoveLeft";
        const string k_AnimMoveRightParameter = "MoveRight";
        const string k_AnimMoveForwardParameter = "MoveForward";
        const string k_AnimMoveBackParameter = "MoveBack";
        const string k_AnimMoveSpeedParameter = "MoveSpeed";
        const string k_AnimIsMovingParameter = "isMoving";
        InputHandler inputHandler;
        private void Start() {
            playerCharacterController = GetComponent<PlayerCharacterController>();
            animator = GetComponent<Animator>();
            inputHandler = GetComponent<InputHandler>();
        }
        private void Update() {
            Vector3 MovementSpeed = playerCharacterController.GetMovementSpeed();
            float moveSpeed = MovementSpeed.sqrMagnitude;
            animator.SetFloat(k_AnimMoveSpeedParameter, moveSpeed);
            if (!inputHandler.GetKeyDown(GameConstants.k_Button_A) && inputHandler.GetKeyDown(GameConstants.k_Button_S) && inputHandler.GetKeyDown(GameConstants.k_Button_D) && inputHandler.GetKeyDown(GameConstants.k_Button_W)) {
                animator.SetBool(k_AnimIsMovingParameter, false);
            } else {
                animator.SetBool(k_AnimIsMovingParameter, true);
            }
            if (inputHandler.GetKeyDown(GameConstants.k_Button_A)) {
                animator.SetBool(k_AnimMoveLeftParameter, true);
            } else if (inputHandler.GetKeyUp(GameConstants.k_Button_A)) {
                animator.SetBool(k_AnimMoveLeftParameter, false);
            }
            if (inputHandler.GetKeyDown(GameConstants.k_Button_S)) {
                animator.SetBool(k_AnimMoveBackParameter, true);
            } else if (inputHandler.GetKeyUp(GameConstants.k_Button_S)) {
                animator.SetBool(k_AnimMoveBackParameter, false);
            }
            if (inputHandler.GetKeyDown(GameConstants.k_Button_W)) {
                animator.SetBool(k_AnimMoveForwardParameter, true);
            } else if (inputHandler.GetKeyUp(GameConstants.k_Button_W)) {
                animator.SetBool(k_AnimMoveForwardParameter, false);
            }
            if (inputHandler.GetKeyDown(GameConstants.k_Button_D)) {
                animator.SetBool(k_AnimMoveRightParameter, true);
            } else if (inputHandler.GetKeyUp(GameConstants.k_Button_D)) {
                animator.SetBool(k_AnimMoveRightParameter, false);
            }
        }
    }
}