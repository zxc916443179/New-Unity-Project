using UnityEngine;

namespace Unity.TPS.Gameplay {
    public class EnemyAnimatorController : MonoBehaviour {
        Animator animator;
        private void Start() {
            animator = GetComponent<Animator>();
        }
        const string k_AnimDamagedParameter = "TakeDamage";
        const string k_AnimDiedParameter = "Died";
        const string k_AnimMoveSpeedParameter = "MoveSpeed";
        const string k_AnimVerticalParameter = "Vertical";
        const string k_AnimHorizontalParameter = "Horizontal";
        public void SetSpeed(float speed) {
            animator.SetFloat(k_AnimMoveSpeedParameter, speed);
        }
        public void SetHorizontal(float horizontal) {
            animator.SetFloat(k_AnimHorizontalParameter, horizontal);
        }
        public void SetVertical(float vertical) {
            animator.SetFloat(k_AnimVerticalParameter, vertical);
        }
        public void SetTakeDamage() {
            animator.SetTrigger(k_AnimDamagedParameter);
        }
        public void SetDied() {
            animator.SetTrigger(k_AnimDiedParameter);
        }
        public bool isPlayingDying() {
            return animator.GetCurrentAnimatorStateInfo(1).IsName("death_A") || animator.GetCurrentAnimatorStateInfo(1).IsName("take_damage_A");
        }
    }
}