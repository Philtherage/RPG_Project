using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;

        Transform target;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if( target != null)
            {
                GetComponent<Mover>().MoveTo(target.position);
                float distance = Vector3.Distance(transform.position, target.position);            
                if (distance <= weaponRange) { GetComponent<Mover>().StopMoving(); }

            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
            print("Attacking target");
        }

        public void Cancel()
        {
            target = null;
        }




    }

}