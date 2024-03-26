using UnityEngine;
using UnityEngine.AI;
using Basic;

namespace BasicAI
{
    public abstract class BasicEnemyAI : MonoBehaviour
    {
        //private
        protected Animator animator;
        private int animIDSpeed;
        private int animIDMotionSpeed;

        //public 
        public NavMeshAgent agent;
        public Transform player;
        public LayerMask whatIsGround, whatIsPlayer;
        public Vector3 walkPoint;
        public bool walkPointSet;
        public float walkPointRange;
        public float timeBetweenAttacks;
        public bool alreadyAttacked;
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        protected virtual void Awake()
        {
            player = GameObject.Find("BasicPlayer").transform;
            agent = GetComponent<NavMeshAgent>();
            AssignAnimationIDs();
        }

        protected virtual void Start()
        {
            animator = GetComponent<Animator>();
            animator.SetFloat(animIDMotionSpeed, 1.0f);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
            Animate();
        }
        private void AssignAnimationIDs()
        {
            animIDSpeed = Animator.StringToHash("Speed");
            animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void Animate()
        {
            animator.SetFloat(animIDSpeed, Mathf.Abs(agent.velocity.x) + Mathf.Abs(agent.velocity.y));
        }
        protected virtual void Patrolling()
        {
            if (!walkPointSet) SearchWalkPoint();
            if (walkPointSet)
            {
                agent.SetDestination(walkPoint);

                Vector3 distanceToWalkPoint = transform.position - walkPoint;
                if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
            }
        }
        protected virtual void SearchWalkPoint()
        {
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            {
                walkPointSet = true;
            }
        }
        protected virtual void ChasePlayer()
        {
            agent.SetDestination(player.position);
        }
        protected virtual void ResetAttack()
        {
            alreadyAttacked = false;
        }
        protected abstract void AttackPlayer();
    }
}
