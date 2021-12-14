using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.Gameplay
{
    public class EventPickup : Pickup
    {
        [Header("Parameters")]
        public UnityEvent events;

        protected override void OnPicked(PlayerCharacterController player)
        {
            events?.Invoke();
        }
    }
}