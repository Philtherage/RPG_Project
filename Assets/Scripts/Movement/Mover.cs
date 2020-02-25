using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        // Update is called once per frame
        void Update()
        {
            UpdateAnimation();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            GetComponent<NavMeshAgent>().SetDestination(destination);
            GetComponent<NavMeshAgent>().isStopped = false;
        }

        public void StopMoving()
        {
            GetComponent<NavMeshAgent>().isStopped = true;
        }

        private void UpdateAnimation()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            GetComponent<Animator>().SetFloat("forwardSpeed", localVelocity.z);
        }

        public void Cancel()
        {
            StopMoving();
        }
    }
}
