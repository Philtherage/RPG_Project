using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    
    NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPoint();
    }

    private void MoveToPoint()
    {
        //Setsup and raycasts to mouse Position then moves player to raycast hit point
        Vector3 mousepos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Ray ray = Camera.main.ViewportPointToRay(mousepos);
        RaycastHit hit;

        if (Input.GetButton("Fire1"))
        {
            Physics.Raycast(ray, out hit);
            navMeshAgent.SetDestination(hit.point);
        }
    }

}
