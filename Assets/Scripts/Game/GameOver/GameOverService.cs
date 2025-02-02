using System;
using System.Collections.Generic;
using Islanders.Game.Buildings_placing;
using Islanders.Game.GameStates;
using Islanders.Game.Player;
using Islanders.Game.UI;
using Zenject;

namespace Islanders.Game.GameOver
{
    public class GameOverService
    {
        #region Variables

        private readonly GameOverScreen _gameOverScreen;
        private readonly PlayerInventory _playerInventory;

        private readonly PlayerScore _playerScore;
        private readonly LocalStateMachine _stateMachine;

        #endregion

        #region Events

        public event Action OnGameOver;

        #endregion

        #region Setup/Teardown

        [Inject]
        public GameOverService(PlayerScore playerScore, PlayerInventory playerInventory, GameOverScreen gameOverScreen,
            LocalStateMachine stateMachine)
        {
            _playerScore = playerScore;
            _playerInventory = playerInventory;
            _gameOverScreen = gameOverScreen;
            _stateMachine = stateMachine;
            _gameOverScreen.OnNextButtonPressed += NextButtonPressedCallback;
            _playerInventory.OnInventoryUpdated += InventoryUpdatedCallback;
        }

        #endregion

        #region Public methods

        public void Destroy()
        {
            _playerInventory.OnInventoryUpdated -= InventoryUpdatedCallback;
        }

        #endregion

        #region Private methods

        private void CheckGameOverConditions()
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

        private void InventoryUpdatedCallback(Dictionary<PlaceableObject, int> inventory, PlaceableObject arg2)
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