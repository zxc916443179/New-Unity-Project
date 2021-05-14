using UnityEngine;
using Unity.TPS.Game;
namespace Unity.TPS.Gameplay {
    public enum JumpState {
        Jumping,
        Land
    }
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
        const string k_AnimJumpedParameter = "Jumped";
        const string k_AnimCloseLandParameter = "CloseLand";
        const string k_AnimResetJumpParameter = "ResetJump";
        const string k_AnimReloadParameter = "Reload";
        InputHandler inputHandler;
        public JumpState jumpState;
        private void Start() {
            playerCharacterController = GetComponent<PlayerCharacterController>();
            animator = GetComponent<Animator>();
            inputHandler = GetComponent<InputHandler>();
            jumpState = JumpState.Land;
        }
        private void Update() {
            Vector3 MovementSpeed = playerCharacterController.GetMovementSpeed();
            float moveSpeed = MovementSpeed.sqrMagnitude;
            animator.SetFloat(k_AnimMoveSpeedParameter, moveSpeed);
            if (!isJumping())
                if (!inputHandler.GetKeyDown(GameConstants.k_Button_A) && !inputHandler.GetKeyDown(GameConstants.k_Button_S) && !inputHandler.GetKeyDown(GameConstants.k_Button_D) && !inputHandler.GetKeyDown(GameConstants.k_Button_W)) {
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
        public void setJumpedAni() {
            print("set trigger");
            jumpState = JumpState.Jumping;
            animator.SetTrigger(k_AnimJumpedParameter);
            return;
        }
        public void setLandAni() {
            animator.SetTrigger(k_AnimCloseLandParameter);
            jumpState = JumpState.Land;
        }
        public bool isJumping() {
            return jumpState == JumpState.Jumping;
        }
        public void resetJump() {
            animator.SetTrigger(k_AnimResetJumpParameter);
            jumpState = JumpState.Land;
        }
        public void setReload() {
            animator.SetTrigger(k_AnimReloadParameter);
        }
        public AnimatorStateInfo GetStateInfo() {
            return animator.GetCurrentAnimatorStateInfo(3);
        }
    }
}