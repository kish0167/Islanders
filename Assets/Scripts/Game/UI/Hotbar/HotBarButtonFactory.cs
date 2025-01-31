using Zenject;

namespace Islanders.Game.UI.Hotbar
{
    public class HotBarButtonFactory
    {
        #region Variables

        private HotBarButton _buttonPrefab;

        private readonly Player.Player _player;

        #endregion

        #region Setup/Teardown

        [Inject]
        public HotBarButtonFactory(Player.Player player)
        {
            _player = player;
        }

        #endregion

        #region Public methods

        public void Setup(HotBarButton button)
        {
            button.Construct(_player);
        }

        #endregion
    }
}