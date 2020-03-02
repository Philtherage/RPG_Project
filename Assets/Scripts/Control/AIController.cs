using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseRange = 5f;

        Transform target;

        // Update is called once per frame
        void Update()
        {
            target = GameObject.FindWithTag("Player").transform;
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if(distance <= chaseRange)
            {
                if(GetComponent<Health>().GetIsDead()) { GetComponent<Mover>().StopMoving(); return; }

                GetComponent<Fighter>().Attack(target.gameObject);
                
                GetComponent<Mover>().MoveTo(target.position);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}