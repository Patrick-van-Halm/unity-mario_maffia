using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

interface IRaycastable3D
{
    public void OnRaycastEnter(RaycastHit hit);
    public void OnRaycastStay(RaycastHit hit);
    public void OnRaycastExit();
}