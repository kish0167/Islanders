using Zenject;

namespace Islanders.Game.UI.Hotbar
{
    public class HotBarButtonFactory
    {
        #region Variables

        private HotBarButton _buttonPrefab;

        private readonly Player.PlayerInventory _playerInventory;

        #endregion

        #region Setup/Teardown

        [Inject]
        public HotBarButtonFactory(Player.PlayerInventory playerInventory)
        {
            _playerInventory = playerInventory;
        }

        #endregion

        #region Public methods

        public void Setup(HotBarButton button)
        {
            button.Construct(_playerInventory);
        }

        #endregion
    }
}