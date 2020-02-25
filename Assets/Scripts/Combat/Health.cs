using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 50;

        public float GetHealth() { return health; }
        
        public void TakeDamage(float damage)
        {
            if(health <= Mathf.Epsilon) { return; }           
            health -= damage;
        }
            
    }


}


