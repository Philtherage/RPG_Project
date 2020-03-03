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
        float timeSinceLastAttack = Mathf.Infinity;

        Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (GetComponent<Health>().GetIsDead()) { Cancel(); }
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
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }

        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            // This will trigger the hit event...
            animator.SetTrigger("attack");
        }

        public void Attack(GameObject combatTarget)
        {
            target = combatTarget.GetComponent<Health>();
            GetComponent<ActionScheduler>().StartAction(this);           
            print("Attacking target");
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        void Hit() // Animation Event
        {           
            if (target == null) return;            
            target.TakeDamage(weaponDamage);         
        }

        public bool CanAttack(GameObject targetObject)
        {
            Health healthCom = targetObject.GetComponent<Health>();
            return targetObject != null && !healthCom.GetIsDead();
        }
    }

}