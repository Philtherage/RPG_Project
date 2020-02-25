using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int health = 50;

        public int GetHealth() { return health; }
        
        public void TakeDamage(int damage)
        {
            health -= damage;
        }
            
    }


}


