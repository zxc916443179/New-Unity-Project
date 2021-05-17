using UnityEngine;
using UnityEngine.Events;
namespace Unity.TPS.Game
{
    public class Health : MonoBehaviour {
        public float maxHealth = 100;
        float currentHealth;
        public UnityAction onDie;
        public UnityAction onDamage;
        bool isDead;
        private void Start() {
            currentHealth = maxHealth;
            isDead = false;
        }
        private void Update() {
            
        }
        public void TakeDamage(float damage) {
            print("take damage: " + damage);
            currentHealth -= damage;
            HandleDeath();
            onDamage?.Invoke();
        }
        void HandleDeath() {
            if (currentHealth <= 0) {
                onDie?.Invoke();
            }
        }
    }
}