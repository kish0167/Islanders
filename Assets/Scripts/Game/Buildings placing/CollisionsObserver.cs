using System;
using UnityEngine;

namespace Islanders.Game.Buildings_placing
{
    public class CollisionsObserver : MonoBehaviour
    {
        #region Events

        public event Action<Collision> OnEnter;
        public event Action<Collision> OnExit;
        public event Action<Collision> OnStay;

        #endregion

        #region Unity lifecycle

        private void OnCollisionEnter(Collision other)
        {
            OnEnter?.Invoke(other);
            Debug.LogError("Enter");
        }

        private void OnCollisionExit(Collision other)
        {
            OnExit?.Invoke(other);
            Debug.LogError("Exit");
        }

        private void OnCollisionStay(Collision other)
        {
            OnStay?.Invoke(other);
            Debug.LogError("Stay");
        }

        #endregion
    }
}