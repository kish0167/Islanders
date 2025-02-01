using System;
using System.Collections.Generic;
using Islanders.Game.Buildings_placing;
using Islanders.Game.Player;
using Zenject;

namespace Islanders.Game.GameOver
{
    public class GameOverService
    {
        #region Variables

        private readonly PlayerScore _playerScore;

        private readonly PlayerInventory _playerInventory;

        #endregion

        #region Events

        public event Action OnGameOver;

        #endregion

        #region Setup/Teardown

        [Inject]
        public GameOverService(PlayerScore playerScore, PlayerInventory playerInventory)
        {
            _playerScore = playerScore;
            _playerInventory = playerInventory;
            playerInventory.OnInventoryUpdated += InventoryUpdatedCallback;
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
            if (_playerInventory.ItemsCount == 0 && _playerScore.Score < _playerScore.ScoreCeiling)
            {
                OnGameOver?.Invoke();
            }
        }

        private void InventoryUpdatedCallback(Dictionary<PlaceableObject, int> inventory, PlaceableObject arg2)
        {
            CheckGameOverConditions();
        }

        #endregion
    }
}