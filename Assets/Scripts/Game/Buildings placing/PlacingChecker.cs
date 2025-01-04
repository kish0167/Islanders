using System;
using UnityEngine;

namespace Islanders.Game.Buildings_placing
{
    public class PlacingChecker : MonoBehaviour
    {
        #region Variables
        
        private PlaceableObject _building;
        private CollisionsObserver _collisionsObserver;
        
        #endregion

        #region Public methods

        public bool IsPossible()
        {
            bool tag = CheckTags();
            bool collider = CheckCollider();

            return tag && collider;
        }

        public void SetBuilding(PlaceableObject building)
        {
            _building = building;
            _collisionsObserver.OnEnter -= CollisionEnterCallback;
            _collisionsObserver.OnEnter -= CollisionExitCallback;
            _collisionsObserver = building.Observer;
        }

        #endregion

        #region Private methods

        private bool CheckCollider()
        {
            
            return false;
        }

        private bool CheckTags()
        {
            
            return false;
        }

        private void CollisionEnterCallback(Collision collision)
        {
            
        }
        
        private void CollisionExitCallback(Collision collision)
        {
            
        }

        #endregion
    }
}