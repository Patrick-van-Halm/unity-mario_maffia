using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parenter : MonoBehaviour
{
    public void ParentObject(Collider col)
    {
        col.transform.SetParent(transform);
    }

    public void UnparentObject(Collider col)
    {
        col.transform.SetParent(null);
    }
}
