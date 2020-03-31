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
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 1f;

        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;
        ActionScheduler actionScheduler;
        

        Vector3 guardPos;
        float timeSinceSeenPlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;
        float waypointArivalTime = Mathf.Infinity;

        private void Start()
        {      
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();

            guardPos = transform.position;
            if(patrolPath != null) { transform.position = patrolPath.GetWaypoint(0); }
            
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
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceSeenPlayer += Time.deltaTime;
            waypointArivalTime += Time.deltaTime;
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

        private void PatrolBehaviour()
        {
            actionScheduler.CancelCurrentAction();

            Vector3 nextPosition = guardPos;
            if(patrolPath != null)
            {             
                if (AtWaypoint())
                {                                      
                    if(waypointArivalTime >= waypointDwellTime)
                    {                        
                        CycleWaypoint();
                    }
                }
                nextPosition = GetCurrentWaypoint();                
            }

            if (timeSinceSeenPlayer >= supisionDelay)
            {
                mover.StartMoveAction(nextPosition);
            }                      
        }
        private bool AtWaypoint()
        {
            return Vector3.Distance(transform.position, GetCurrentWaypoint()) < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
            waypointArivalTime = 0f;
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

    }




}