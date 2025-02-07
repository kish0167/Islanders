using System;
using Islanders.Game.Buildings_placing;
using Islanders.Game.ScoreHandling;
using Islanders.Game.UI;
using Islanders.Game.UI.Hotbar;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Islanders.Game.Undo
{
    public class UndoService
    {
        #region Variables

        private readonly GameOverScreen _gameOverScreen;
        private readonly HotBar _hotBar;
        private readonly PlaceableObjectFactory _placeableObjectFactory;
        private readonly BuildingsPlacer _placer;
        private bool _available;

        public bool Available => _available;

        private int _lastAcquiredScore;

        private PlaceableObject _lastPlacedObject;
        private PlaceableObject _lastPlacedObjectPrefab;

        #endregion

        #region Events

        public event Action<int, PlaceableObject> OnPlacingUndone;

        #endregion

        #region Setup/Teardown

        [Inject]
        public UndoService(HotBar hotBar, GameOverScreen gameOverScreen, BuildingsPlacer placer,
            PlaceableObjectFactory placeableObjectFactory)
        {
            _hotBar = hotBar;
            _gameOverScreen = gameOverScreen;
            _placer = placer;
            _placeableObjectFactory = placeableObjectFactory;

            ScoreCounter.OnScoreAcquiring += ScoreAcquiringCallback;
            _placer.OnBuildingPlaced += BuildingPlacedCallback;

            _hotBar.OnUndoButtonPressed += UndoButtonPressedCallback;
            _gameOverScreen.OnUndoButtonPressed += UndoButtonPressedCallback;
        }

        #endregion

        #region Private methods

        private void BuildingPlacedCallback(PlaceableObject building, PlaceableObject prefab, Vector3 arg2)
        {
            _lastPlacedObject = building;
            _lastPlacedObjectPrefab = prefab;
            _available = true;
        }

        private void ScoreAcquiringCallback(int value)
        {
            _lastAcquiredScore = value;
        }

        private void UndoButtonPressedCallback()
        {
            //_placeableObjectFactory.Deconstruct(_lastPlacedObject);
            Object.Destroy(_lastPlacedObject.gameObject);
            _available = false;
            OnPlacingUndone?.Invoke(_lastAcquiredScore, _lastPlacedObjectPrefab);
            _lastAcquiredScore = 0;
            _lastPlacedObject = null;
        }

        #endregion
    }
}