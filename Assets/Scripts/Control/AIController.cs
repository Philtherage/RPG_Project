using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseRange = 5f;
        [SerializeField] float supisionDelay = 2f;

        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;
        ActionScheduler actionScheduler;



        Vector3 guardPos;
        float timeSinceSeenPlayer = Mathf.Infinity;

        private void Start()
        {      
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();

            guardPos = transform.position;
        }

        void Update()
        {
            if (health.GetIsDead()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceSeenPlayer = 0f;
                fighter.Attack(player);                
            }
            else
            {            
                Evade();
            }
            timeSinceSeenPlayer += Time.deltaTime;
        }

        private bool InAttackRangeOfPlayer()
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            return distance <= chaseRange;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }

        private void Evade()
        {
            actionScheduler.CancelCurrentAction();

            if (timeSinceSeenPlayer >= supisionDelay)
            {
                mover.StartMoveAction(guardPos);
            }                      
        }

    }


}