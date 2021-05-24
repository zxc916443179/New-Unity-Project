using UnityEngine;
using UnityEngine.Events;
namespace Unity.TPS.Game
{
    public class Health : MonoBehaviour {
        public float maxHealth = 100;
        float currentHealth;
        public UnityAction<GameObject> onDie;
        public UnityAction onDamage;
        bool isDead;
        private void Start() {
            currentHealth = maxHealth;
            isDead = false;
        }
        private void Update() {
            
        }
        public bool TakeDamage(float damage, string tag) {
            if (tag == transform.tag) {
                return false;
            }
            if (!isDead) {
                print("take damage: " + damage);
                currentHealth -= damage;
                if (currentHealth < 0) currentHealth = 0;
                HandleDeath();
                onDamage?.Invoke();
            }
            return true;
        }
        void HandleDeath() {
            if (currentHealth <= 0) {
                onDie?.Invoke(this.gameObject);
                isDead = true;
            }
        }
        public float GetCurrentHealth() {
            return currentHealth;
        }
    }
}