using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Raycaster : MonoBehaviour
{
    public Transform origin;
    public float distance;

    private RaycastHit hit;

    private void Update()
    {
        if (Physics.Raycast(origin.position, origin.forward, out RaycastHit newHit, distance))
        {
            //print(newHit.collider.name);
            if (hit.collider == newHit.collider)
            {
                hit.collider.GetComponent<IRaycastable3D>()?.OnRaycastStay(newHit);
                hit = newHit;
                return;
            }

            if (hit.collider) hit.collider.GetComponent<IRaycastable3D>()?.OnRaycastExit();
            hit = newHit;
            hit.collider.GetComponent<IRaycastable3D>()?.OnRaycastEnter(hit);
        }
        else
        {
            if (!hit.collider) return;
            hit.collider.GetComponent<IRaycastable3D>()?.OnRaycastExit();
            hit = default;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(origin.position, origin.position + (origin.forward * distance));
    }
}