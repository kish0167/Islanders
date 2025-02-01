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

        public static event Action<Transform, int> OnPreScoreCalculated; // для "предпоказа" очков
        public static event Action<Dictionary<Transform, int>>
            OnPreScoreDrawing; // для UI чисел над постройками на сцене

        public static event Action<int> OnScoreAcquiring; // для начисления очков

        #endregion

        #region Properties

        public float Radius => _radius;

        public ObjectType Type => _objectType;

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            if (_isPlaced)
            {
                return;
            }

            CastASphereAndCalculateScore();
        }

        #endregion

        #region Public methods

        public void Construct(ScoreTableService tableService, BuildingsPlacer placer)
        {
            _placer = placer;
            _ownScoreMap = tableService.GetDictionaryForType(_objectType);
            _placer.OnBuildingPlaced += BuildingPlacedCallback;
        }

        public void Deconstruct()
        {
            _placer.OnBuildingPlaced -= BuildingPlacedCallback;
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

            OnPreScoreCalculated?.Invoke(transform, _currentScore);
            OnPreScoreDrawing?.Invoke(numbersToShow);
        }

        #endregion
    }
}