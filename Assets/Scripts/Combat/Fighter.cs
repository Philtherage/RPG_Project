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

        Health target;
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
            
            if (target != null)
            {
                if (target.GetIsDead()) { Cancel(); return; }
                transform.LookAt(target.transform);
                GetComponent<Mover>().MoveTo(target.transform.position);
                InRange();
            }
            
        }

        private void InRange()
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
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
            target = combatTarget.GetComponent<Health>();
            GetComponent<ActionScheduler>().StartAction(this);           
            print("Attacking target");
        }

        public void Cancel()
        {
            animator.SetTrigger("stopAttack");
            target = null;
        }

        void Hit() // Animation Event
        {           
            if (target == null) return;            
            target.TakeDamage(weaponDamage);         
        }

        public bool CanAttack()
        {
            return target != null && !target.GetIsDead();
        }
    }

}