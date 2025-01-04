using System;
using UnityEngine;

namespace Islanders.Game.Buildings_placing
{
    public class CollisionsObserver : MonoBehaviour
    {
        public event Action<Collision> OnEnter; 
        public event Action<Collision> OnExit; 
        private void OnCollisionEnter(Collision other)
        {
            OnEnter?.Invoke(other);
        }

        private void OnCollisionExit(Collision other)
        {
            OnExit?.Invoke(other);
        }
    }
}