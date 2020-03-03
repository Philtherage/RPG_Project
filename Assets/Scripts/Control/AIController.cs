using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseRange = 5f;

        GameObject player;
        Fighter fighter;
        Health health;

        private void Start()
        {      
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (health.GetIsDead()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {               
                fighter.Attack(player);                
            }
            else
            {
                GetComponent<Fighter>().Cancel(); 
            }
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
    }
}