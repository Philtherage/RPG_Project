﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;

        Transform target;

        // Update is called once per frame
        void Update()
        {
            if(target != null)
            {
                GetComponent<Mover>().MoveTo(target.position);
                InRange();
            }
        }

        private void InRange()
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= weaponRange)
            {
                GetComponent<Mover>().StopMoving();
                GetComponent<Animator>().SetTrigger("attack");
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
            GetComponent<ActionScheduler>().StartAction(this);           
            print("Attacking target");
        }

        public void Cancel()
        {
            target = null;
            //GetComponent<Animator>().SetTrigger("");
        }
    }

}