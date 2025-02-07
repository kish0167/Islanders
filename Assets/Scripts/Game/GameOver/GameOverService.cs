using System;
using System.Collections;
using System.Collections.Generic;
using Islanders.Game.Buildings_placing;
using Islanders.Game.GameStates;
using Islanders.Game.Player;
using Islanders.Game.UI;
using Islanders.Game.Undo;
using UnityEditor.VersionControl;
using UnityEngine;
using Zenject;
using Task = System.Threading.Tasks.Task;

namespace Islanders.Game.GameOver
{
    public class GameOverService
    {
        #region Variables

        private readonly GameOverScreen _gameOverScreen;
        private readonly UndoService _undoService;
        private readonly BuildingsPlacer _placer;
        private readonly PlayerInventory _playerInventory;
        
        private readonly PlayerScore _playerScore;
        private readonly LocalStateMachine _stateMachine;

        #endregion

        #region Events

        public event Action OnGameOver;

        #endregion

        #region Setup/Teardown

        [Inject]
        public GameOverService(PlayerScore playerScore, GameOverScreen gameOverScreen,
            LocalStateMachine stateMachine, BuildingsPlacer placer, PlayerInventory playerInventory, UndoService undoService)
        {
            _playerScore = playerScore;
            _gameOverScreen = gameOverScreen;
            _stateMachine = stateMachine;
            _placer = placer;
            _playerInventory = playerInventory;
            _undoService = undoService;
            _gameOverScreen.OnNextButtonPressed += NextButtonPressedCallback;
            _placer.OnBuildingPlacedLate += InventoryUpdatedCallback;
            _undoService.OnPlacingUndone += PlacingUndoneCallback;
        }

        private void PlacingUndoneCallback(int arg1, PlaceableObject arg2)
        {
            CheckGameOverConditions();
        }

        #endregion

        #region Public methods

        public void Destroy()
        {
            _placer.OnBuildingPlacedLate -= InventoryUpdatedCallback;
        }

        #endregion

        #region Private methods

        public void CheckGameOverConditions()
        {
            if (_playerInventory.ItemsCount != 0 || _playerScore.Score >= _playerScore.ScoreCeiling)
            {
                if (_stateMachine.Is<GameOverState>())
                {
                    _stateMachine.TransitionTo<PlacingState>();
                }

                return;
            }

            OnGameOver?.Invoke();
            _stateMachine.TransitionTo<GameOverState>();
        }

        private void InventoryUpdatedCallback()
        {
            CheckGameOverConditions();
        }

        private void NextButtonPressedCallback()
        {
            _stateMachine.TransitionTo<GoToNewIslandState>();
        }

        

        #endregion
    }
}