using Islanders.Game.Buildings_placing;
using Zenject;

namespace Islanders.Game.UI.Hotbar
{
    public class HotBarButtonFactory
    {
        #region Variabless

        private HotBarButton _buttonPrefab;

        private BuildingsPlacer _placer;

        #endregion

        #region Setup/Teardown

        [Inject]
        public HotBarButtonFactory(BuildingsPlacer placer)
        {
            _placer = placer;
        }

        #endregion

        #region Public methods

        public void Setup(HotBarButton button)
        {
            button.Construct(_placer);
        }

        #endregion
    }
}