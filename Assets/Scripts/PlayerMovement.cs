using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    
    NavMeshAgent navMeshAgent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPoint();
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        animator.SetFloat("forwardSpeed", localVelocity.z);
    }

    private void MoveToPoint()
    {
        //Setsup and raycasts to mouse Position then moves player to raycast hit point
        Vector3 mousepos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Ray ray = Camera.main.ViewportPointToRay(mousepos);
        RaycastHit hit;

        if (Input.GetButton("Fire1"))
        {
            bool hitSomething = Physics.Raycast(ray, out hit);
            if (hitSomething)
            {
                navMeshAgent.SetDestination(hit.point);

            }
        }
    }

}
