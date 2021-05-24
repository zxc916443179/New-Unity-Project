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
        const string k_AnimJumpedParameter = "Jumped";
        const string k_AnimCloseLandParameter = "CloseLand";
        const string k_AnimResetJumpParameter = "ResetJump";
        const string k_AnimReloadParameter = "Reload";
        const string k_AnimIsWalkingParameter = "IsWalking";
        const string k_AnimIsRunningParameter = "IsRunning";
        const string k_AnimIsCrouchingParameter = "IsCrouching";
        const string k_AnimHorizontalParameter = "Horizontal";
        const string k_AnimVerticalParameter = "Vertical";
        const string k_AnimAimAngleParameter = "AimAngle";
        const string k_AnimShootParameter = "Shoot";
        const string k_AnimBurstShootParameter = "BurstShoot";
        const string k_AnimAutoShootParameter = "AutoShoot";
        const string k_AnimDieParameter = "Die";
        InputHandler inputHandler;
        public JumpState jumpState;
        public int ReloadAniLayerIndex = 3;
        float angle;
        private void Start() {
            playerCharacterController = GetComponent<PlayerCharacterController>();
            animator = GetComponent<Animator>();
            inputHandler = GetComponent<InputHandler>();
            jumpState = JumpState.Land;
        }
        private void Update() {
            animator.SetBool(k_AnimIsCrouchingParameter, inputHandler.isCrouching);
            animator.SetBool(k_AnimIsWalkingParameter, inputHandler.isWalking);
            angle = Camera.main.transform.eulerAngles.x - 180;
            if (angle > 0) {
                angle -= 180;
            } else {
                angle += 180;
            }
            animator.SetFloat(k_AnimAimAngleParameter, angle);
            if (!isJumping()) {
                Vector3 moveVec = inputHandler.GetMoveInput();
                animator.SetFloat(k_AnimVerticalParameter, moveVec.z);
                animator.SetFloat(k_AnimHorizontalParameter, moveVec.x);
            }
                
        }
        public void setJumpedAni() {
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
            return animator.GetCurrentAnimatorStateInfo(ReloadAniLayerIndex);
        }

        public void setShoot(bool isBursting) {
            if (isBursting) {
                animator.SetTrigger(k_AnimBurstShootParameter);
            } else {
                animator.SetTrigger(k_AnimShootParameter);
            }
        }
        public void SetDie() {
            animator.SetTrigger(k_AnimDieParameter);
        }
    }
}