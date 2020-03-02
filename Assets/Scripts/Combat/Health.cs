using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
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
            Die();
        }

        private void Die()
        {
            if (isDead) return;
            if (healthPoints == 0)
            {
                isDead = true;
                animator.SetTrigger("dead");
            }
        }

    }
}


