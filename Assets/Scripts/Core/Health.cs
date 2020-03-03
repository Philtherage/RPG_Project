using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 50;


        bool isDead = false;


        Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public bool GetIsDead() { return isDead; }

        public float GetHealth() { return healthPoints; }
        
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
                        
            isDead = true;
            animator.SetTrigger("dead");
            GetComponent<ActionScheduler>().CancelCurrentAction();           
        }

    }
}


