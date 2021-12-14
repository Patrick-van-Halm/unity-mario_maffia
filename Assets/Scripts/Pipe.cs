using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Pipe : MonoBehaviour, IRaycastable3D
{
    public Transform[] EndPointsTransforms;
    public Vector3 HalfExtends;
    public PipeTypes PipeType;
    public GameObject door;

    public enum PipeTypes
    {
        Default,
        Start,
        End
    }

    public List<Pipe> ConnectedTo = new List<Pipe>();
    public bool canRotate;

    private bool isFinished = true;

    void Start()
    {
        UpdateConnections();
    }

    void Update()
    {
        if (PipeType == PipeTypes.Start && !isFinished && CanTraceToEnd())
        {
            isFinished = true;
            door.SetActive(false);
        }
        else if (PipeType == PipeTypes.Start && isFinished && !CanTraceToEnd()) 
        {
            isFinished = false;
            door.SetActive(true);
        }
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles + Vector3.up * 90);
    }

    private void UpdateConnections()
    {
        List<Pipe> newConnections = new List<Pipe>();
        for (int i = 0; i < EndPointsTransforms.Length; i++)
        {
            //Use the OverlapBox to detect if there are any other colliders within this box area.
            //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
            Collider[] hitColliders = Physics.OverlapBox(EndPointsTransforms[i].position, HalfExtends, Quaternion.identity);
            if (hitColliders.Length > 0)
                newConnections.AddRange(hitColliders.Where(col => col.CompareTag("Pipe") && col.gameObject != gameObject).Select(col => col.GetComponent<Pipe>()));
        }
        for (int i = 0; i < ConnectedTo.Count; i++)
        {
            if(!newConnections.Contains(ConnectedTo[i])) ConnectedTo[i].RemConnection(this);
        }

        ConnectedTo = newConnections;

        for (int i = 0; i < ConnectedTo.Count; i++)
        {
            ConnectedTo[i].AddConnection(this);
        }
    }

    public void AddConnection(Pipe pipe)
    {
        if(!ConnectedTo.Contains(pipe)) ConnectedTo.Add(pipe);
    }

    public void RemConnection(Pipe pipe)
    {
        if (ConnectedTo.Contains(pipe)) ConnectedTo.Remove(pipe);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < EndPointsTransforms.Length; i++)
        {
            Gizmos.DrawWireCube(EndPointsTransforms[i].position, HalfExtends * 2);
        }
    }

    private bool CanTraceToEnd(Pipe pipe = null, Pipe prevPipe = null)
    {
        if (!pipe) pipe = this;
        
        if (pipe.ConnectedTo.Count > 0)
        {
            for (int i = 0; i < pipe.ConnectedTo.Count; i++)
            {
                if(prevPipe && prevPipe == pipe.ConnectedTo[i]) continue;
                if (CanTraceToEnd(pipe.ConnectedTo[i], pipe))
                {
                    return true;
                }
            }
        }
        return pipe.PipeType == PipeTypes.End;
    }

    public void OnRaycastEnter(RaycastHit hit)
    {
        //print("Hit enter");
        //throw new System.NotImplementedException();
    }

    public void OnRaycastStay(RaycastHit hit)
    {
        if (canRotate && Input.GetMouseButtonDown(0))
        {
            Rotate();
            UpdateConnections();
        }
    }

    public void OnRaycastExit()
    {
        //print("Hit exit");
    }
}