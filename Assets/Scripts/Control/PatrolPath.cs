using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float waypointGizmoRadius = .5f;

        private void OnDrawGizmos()
        {
            int length = transform.childCount;
            
            
            for(int i = 0; i < length; i++)
            {
                int nextPoint = GetNextIndex(i, length);

                Gizmos.color = Color.red;
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);

                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(nextPoint));

            }

        }

        private static int GetNextIndex(int i, int length)
        {
            int j;

            if(i == length - 1)
            {
                j = 0;              
            }
            else
            {
                j = i + 1;
            }
            return j;
        }

        private Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

    }
}

