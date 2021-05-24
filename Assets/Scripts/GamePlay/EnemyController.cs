
using UnityEngine;
using Unity.TPS.Game;
using Unity.TPS.Audio;

namespace Unity.TPS.Gameplay {
    public class EnemyController : MonoBehaviour
    {
        private GameObject target;
        [SerializeField]AudioController audioController;
        Vector3 previousPosition;
        private UnityEngine.AI.NavMeshAgent agent;
        // Start is called before the first frame update

        
        const string k_AnimMoveSpeedParameter = "MoveSpeed";
        EnemyAnimatorController enemyAnimatorController;
        EnemyWeaponController enemyWeaponController;
        Health health;
        [SerializeField]float minimumAttackDistance = 10f;
        public Vector3 moveSpeed;
        public float fixLookAt = 5;
        public float minimumMoveThreshold = 0.2f;
        void Start()
        {
            target = GameObject.FindWithTag("Player");
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            enemyAnimatorController = GetComponent<EnemyAnimatorController>();
            enemyWeaponController = GetComponent<EnemyWeaponController>();
            health = GetComponent<Health>();
            health.onDie += onDie;
            health.onDamage += onDamage;

            previousPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameObject.FindWithTag("Player") == null) return;
            moveSpeed = agent.velocity;
            enemyAnimatorController.SetHorizontal(Mathf.Clamp(agent.velocity.x, -1, 1));
            enemyAnimatorController.SetVertical(Mathf.Clamp(agent.velocity.z, -1, 1));
            transform.LookAt(new Vector3(target.transform.position.x + fixLookAt, target.transform.position.y, target.transform.position.z));
            if (Vector3.Distance(target.transform.position, transform.position) <= minimumAttackDistance) {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                enemyWeaponController.Attack();
            } else {
                agent.isStopped = false;
                agent.SetDestination(target.transform.position);
            }
            if (Vector3.Distance(previousPosition, transform.position) >= minimumAttackDistance && CanAttack()) {
                print("playing");
                audioController.Play();
            } else {
                audioController.Stop();
            }
            previousPosition = transform.position;
        }
        void onDie(GameObject GO) {
            enemyAnimatorController.SetDied();
            Invoke("DestroySelf", 3f);

        }
        void onDamage() {
            enemyAnimatorController.SetTakeDamage();
        }
        void DestroySelf() {
            Destroy(this.gameObject);
        }

        bool CanAttack() {
            return health.GetCurrentHealth() > 0 && GameObject.FindWithTag("Player") != null;
        }
    }
}