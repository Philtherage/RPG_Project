using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        

        // Update is called once per frame
        void Update()
        {
            InteractWithMovement();
            InteractWithCombat();
        }

        private void InteractWithMovement()
        {
            RaycastHit hit;
            if (Input.GetButton("Fire1"))
            {
                bool hitSomething = Physics.Raycast(GetMouseRay(), out hit);
                if (hitSomething)
                {
                    GetComponent<Mover>().MoveTo(hit.point);
                }
            }
        }

        private static Ray GetMouseRay()
        {
            //Setsup and raycasts to mouse Position then moves player to raycast hit point
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (Input.GetButtonDown("Fire1"))
                {
                    GetComponent<Fighter>().Attack(target);
                }
            }         
        }


    }
}
