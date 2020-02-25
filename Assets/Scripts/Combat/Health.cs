using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 50;


        bool dead = false;


        Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public float GetHealth() { return healthPoints; }
        
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);          
            Die();
        }

        private void Die()
        {
            if (dead) return;
            if (healthPoints == 0)
            {
                dead = true;
                animator.SetTrigger("dead");
            }
        }

    }


}


