using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace UnityUtils
{
    [RequireComponent(typeof(Collider))]
    public class TriggerHandler3D_Event : MonoBehaviour
    {
        [Space]
        [Header("=== On Trigger Enter ===")]
        public UnityEvent onTriggerEnter;
        public UnityEvent<Collider> onTriggerEnterWithCollider;

        [Space]
        [Header("=== On Trigger Stay ===")]
        public UnityEvent onTriggerStay;
        public UnityEvent<Collider> onTriggerStayWithCollider;

        [Space]
        [Header("=== On Trigger Exit ===")]
        public UnityEvent onTriggerExit;
        public UnityEvent<Collider> onTriggerExitWithCollider;

        private void OnTriggerEnter(Collider col)
        {
            onTriggerEnter?.Invoke();
            onTriggerEnterWithCollider?.Invoke(col);
        }

        private void OnTriggerStay(Collider col)
        {
            onTriggerStay?.Invoke();
            onTriggerStayWithCollider?.Invoke(col);
        }

        private void OnTriggerExit(Collider col)
        {
            onTriggerExit?.Invoke();
            onTriggerExitWithCollider?.Invoke(col);
        }
    }
}
