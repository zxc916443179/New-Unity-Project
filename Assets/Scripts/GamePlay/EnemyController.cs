
using UnityEngine;
using Unity.TPS.Game;
namespace Unity.TPS.Gameplay {
    public class EnemyController : MonoBehaviour
    {
        private GameObject target;
        private UnityEngine.AI.NavMeshAgent agent;
        // Start is called before the first frame update

        
        const string k_AnimMoveSpeedParameter = "MoveSpeed";
        EnemyAnimatorController enemyAnimatorController;
        Health health;
        void Start()
        {
            target = GameObject.FindWithTag("Player");
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            enemyAnimatorController = GetComponent<EnemyAnimatorController>();
            health = GetComponent<Health>();
            health.onDie += onDie;
            health.onDamage += onDamage;
        }

        // Update is called once per frame
        void Update()
        {
            float moveSpeed = agent.velocity.magnitude;
            enemyAnimatorController.SetSpeed(moveSpeed);
        }
        void onDie() {
            enemyAnimatorController.SetDied();
            Invoke("DestroySelf", 3f);

        }
        void onDamage() {
            enemyAnimatorController.SetTakeDamage();
        }
        void DestroySelf() {
            Destroy(this.gameObject);
        }
    }
}