using System;
using System.Collections.Generic;
using Islanders.Game.Buildings_placing;
using Islanders.Game.Utility;
using UnityEngine;

namespace Islanders.Game.ScoreHandling
{
    public class ScoreCounter : MonoBehaviour
    {
        #region Variables

        [SerializeField] private ObjectType _objectType;
        [SerializeField] private float _radius;
        private int _currentScore;
        private bool _isPlaced;
        private Dictionary<ObjectType, int> _ownScoreMap;
        private BuildingsPlacer _placer;

        #endregion

        #region Events

        public static event Action<int> OnScoreAcquiring; // для начисления очков
        public static event Action<int> OnScoreCalculated; // для "предпоказа" очков
        public static event Action<Dictionary<Transform, int>> OnSphereCasted; // для UI чисел над постройками на сцене

        #endregion

        #region Properties

        public ObjectType Type => _objectType;
        public float Radius => _radius;

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            if (_isPlaced)
            {
                return;
            }

            CastASphereAndCalculateScore();

            Debug.Log(_currentScore);
        }

        public void OnDrawGizmos()
        {
            if (_isPlaced)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, _radius);
        }

        #endregion

        #region Public methods

        public void Construct(ScoreService service, BuildingsPlacer placer)
        {
            _placer = placer;
            _ownScoreMap = service.GetDictionaryForType(_objectType);
            _placer.OnBuildingPlaced += BuildingPlacedCallback;
            Debug.Log("score counter constructed!");
        }

        #endregion

        #region Private methods

        private void BuildingPlacedCallback(PlaceableObject arg1, Vector3 arg2)
        {
            _isPlaced = true;
            OnScoreAcquiring?.Invoke(_currentScore);
            _placer.OnBuildingPlaced -= BuildingPlacedCallback;
        }

        private void CastASphereAndCalculateScore()
        {
            Collider[] hits;
            hits = Physics.OverlapSphere(gameObject.transform.position, _radius,
                LayerMask.GetMask(Layers.PlacedBuilding));

            Dictionary<Transform, int> numbersToShow = new();

            _currentScore = 0;

            foreach (Collider hit in hits)
            {
                if (!hit.gameObject.TryGetComponent(out ScoreCounter scoreCounter))
                {
                    Debug.LogError(hit.gameObject.name + " somehow missing " + nameof(ScoreCounter));
                }

                numbersToShow.Add(hit.gameObject.transform, _ownScoreMap[scoreCounter.Type]);
                _currentScore += _ownScoreMap[scoreCounter.Type];
            }

            OnScoreCalculated?.Invoke(_currentScore);
            OnSphereCasted?.Invoke(numbersToShow);
        }

        #endregion
    }
}