using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (health.GetIsDead()) { return; }
            if (InteractWithCombat()) { return; }
            if (!InteractWithMovement()) { return; }
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hitSomething = Physics.Raycast(GetMouseRay(), out hit);
            if (Input.GetButton("Fire1") && hitSomething)
            {
                GetComponent<Mover>().StartMoveAction(hit.point);
            }
            return hitSomething;
        }

        private static Ray GetMouseRay()
        {
            //Setsup and raycasts to mouse Position then moves player to raycast hit point
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;
                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                if (Input.GetButtonDown("Fire1"))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);                   
                }
                return true;
            }
            return false;
        }


    }
}
