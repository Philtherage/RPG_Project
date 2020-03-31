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
                int nextPoint = GetNextIndex(i);

                Gizmos.color = Color.red;
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);

                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(nextPoint));

            }

        }

        public int GetNextIndex(int i)
        {
            int j;

            if(i == transform.childCount - 1)
            {
                j = 0;              
            }
            else
            {
                j = i + 1;
            }
            return j;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

    }
}

