
using UnityEngine;
namespace Unity.TPS.Game{
    public class EnemyController : MonoBehaviour
    {
        private GameObject target;
        private UnityEngine.AI.NavMeshAgent agent;
        // Start is called before the first frame update

        
        const string k_AnimMoveSpeedParameter = "MoveSpeed";
        Animator animator;
        void Start()
        {
            target = GameObject.FindWithTag("Player");
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            animator = GetComponent<Animator>();
            
        }

        // Update is called once per frame
        void Update()
        {
            float moveSpeed = agent.velocity.magnitude;
            animator.SetFloat(k_AnimMoveSpeedParameter, moveSpeed);
            agent.SetDestination(target.transform.position);
        }
    }
}