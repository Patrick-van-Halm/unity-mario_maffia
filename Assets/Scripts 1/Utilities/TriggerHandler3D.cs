using System.Reflection;
using UnityEngine;

namespace UnityUtils
{
    [RequireComponent(typeof(Collider))]
    public class TriggerHandler3D : MonoBehaviour
    {
        public Component target;

        public string methodEnterName;
        private MethodInfo methodEnter;
        public string methodStayName;
        private MethodInfo methodStay;
        public string methodExitName;
        private MethodInfo methodExit;

        private void OnValidate()
        {
            if (!target) return;
            if (methodEnterName != null) methodEnter = target.GetType().GetMethod(methodEnterName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodStayName != null) methodStay = target.GetType().GetMethod(methodStayName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodExitName != null) methodExit = target.GetType().GetMethod(methodExitName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        }

        private void OnTriggerEnter(Collider col)
        {
            if (methodEnter == null) return;
            if (methodEnter.GetParameters().Length == 1)
                methodEnter.Invoke(target, new object[] { col });
            else
                methodEnter.Invoke(target, new object[] { });
        }

        private void OnTriggerStay(Collider col)
        {
            if (methodStay == null) return;
            if (methodStay.GetParameters().Length == 1)
                methodStay.Invoke(target, new object[] { col });
            else
                methodStay.Invoke(target, new object[] { });
        }

        private void OnTriggerExit(Collider col)
        {
            if (methodExit == null) return;
            if (methodExit.GetParameters().Length == 1)
                methodExit.Invoke(target, new object[] { col });
            else
                methodExit.Invoke(target, new object[] { });
        }
    }
}
