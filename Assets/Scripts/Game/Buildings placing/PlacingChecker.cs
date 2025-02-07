using System;
using Islanders.Game.Utility;
using UnityEngine;

namespace Islanders.Game.Buildings_placing
{
    public class PlacingChecker : MonoBehaviour
    {
        #region Variables

        
        [Header("Options")]
        [SerializeField] private float _linecastDepth = 5;
        [SerializeField] private float _pivotGap = 0.1f;

        private PlaceableObject _building;
        private int _collisionsCount;
        private CollisionsObserver _collisionsObserver;

        private string _currentTerrainTag;
        private bool _isColliding;

        #endregion

        #region Public methods

        public bool IsPossible()
        {
            bool tagAllowed = CheckTags();
            bool collisionAllowed = CheckCollider();

            return tagAllowed && collisionAllowed;
        }

        public void Reset()
        {
            _collisionsCount = 0;
        }

        public void SetBuilding(PlaceableObject building)
        {
            _building = building;
            _collisionsCount = 0;

            if (_collisionsObserver != null)
            {
                _collisionsObserver.OnStay -= CollisionStayCallback;
                _collisionsObserver.OnEnter -= CollisionEnterCallback;
                _collisionsObserver.OnExit -= CollisionExitCallback;
            }

            _collisionsObserver = _building.Observer;
            _collisionsObserver.OnStay += CollisionStayCallback;
            _collisionsObserver.OnEnter += CollisionEnterCallback;
            _collisionsObserver.OnExit += CollisionExitCallback;
        }

        #endregion

        #region Private methods

        private bool CheckCollider()
        {
            return _collisionsCount == 0;
        }

        private bool CheckTags()
        {
            Vector3 v1 = _building.transform.position - Vector3.down * _pivotGap;
            Vector3 v2 = v1 + _building.transform.TransformDirection(_building.LinecastDirection).normalized * _linecastDepth;
            

            if (!Physics.Linecast(v1, v2, out RaycastHit hit, ~LayerMask.GetMask(Layers.ActiveBuilding)))
            {
                return false;
            }
            
            foreach (string allowedTag in _building.AllowedTags)
            {
                if (string.Equals(allowedTag, hit.collider.gameObject.tag))
                {
                    return true;
                }
            }
            
            return false;
        }

        private void CollisionEnterCallback(Collision collision)
        {
            _collisionsCount++;
        }

        private void CollisionExitCallback(Collision collision)
        {
            _collisionsCount--;
        }

        private void CollisionStayCallback(Collision collision) { }

        #endregion
    }
}