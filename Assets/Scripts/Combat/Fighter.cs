using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Transform target;
        float timeSinceLastAttack = 0;

        Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if(target != null)
            {
                GetComponent<Mover>().MoveTo(target.position);
                InRange();
            }
        }

        private void InRange()
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= weaponRange)
            {
                GetComponent<Mover>().StopMoving();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                // This will trigger the hit event...
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
            
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
            GetComponent<ActionScheduler>().StartAction(this);           
            print("Attacking target");
        }

        public void Cancel()
        {
            target = null;
        }

        void Hit() // Animation Event
        {
            target.GetComponent<Health>().TakeDamage(weaponDamage);          
        }
    }

}