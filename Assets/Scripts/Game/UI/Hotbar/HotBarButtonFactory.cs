using Islanders.Game.Buildings_placing;
using Zenject;

namespace Islanders.Game.UI.Hotbar
{
    public class HotBarButtonFactory
    {
        #region Variabless

        private HotBarButton _buttonPrefab;

        private Player.Player _player;

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