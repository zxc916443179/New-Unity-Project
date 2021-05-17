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
        public void SetSpeed(float speed) {
            animator.SetFloat(k_AnimMoveSpeedParameter, speed);
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