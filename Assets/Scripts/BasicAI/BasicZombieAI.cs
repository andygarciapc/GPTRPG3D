using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Basic;


namespace BasicAI
{
    public class BasicZombieAI : BasicEnemyAI
    {
        private BasicFighter basicFighter;
        protected override void Awake()
        {
            base.Awake();
        }
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            basicFighter = GetComponent<BasicFighter>();
            basicFighter.AttackStance = true;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        protected override void AttackPlayer()
        {
            agent.SetDestination(transform.position);
            if (!alreadyAttacked)
            {
                //Debug.Log("AI Attacking");
                //TODO: Implement attack code
                basicFighter.DoAttack();

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
        protected override void ChasePlayer()
        {
            base.ChasePlayer();
        }
        protected override void Patrolling()
        {
            base.Patrolling();
        }
        protected override void SearchWalkPoint()
        {
            base.SearchWalkPoint();
        }
    }
}