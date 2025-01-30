using Islanders.Game.Buildings_placing;
using Islanders.Game.Player;
using Islanders.Game.UI;
using Zenject;

namespace Islanders.Game.GameStates
{
    public class BootsTrapState : GameState
    {
        #region Variables

        private MenuScreen _menuScreen;
        private ChoiceScreen _choiceScreen;
        private readonly BuildingsPlacer _placer;
        private readonly PlayerScore _playerScore;
        private PlacingScreen _placingScreen;

        #endregion

        #region Setup/Teardown

        [Inject]
        public BootsTrapState(MenuScreen menuScreen, PlayerScore playerScore, BuildingsPlacer placer, PlacingScreen placingScreen, ChoiceScreen choiceScreen)
        {
            _menuScreen = menuScreen;
            _playerScore = playerScore;
            _placer = placer;
            _placingScreen = placingScreen;
            _choiceScreen = choiceScreen;
        }

        #endregion

        #region Public methods

        public override void Enter()
        {
            _placer.Enabled = false;
            _playerScore.SetToZero();
            _menuScreen.Hide();
            _placingScreen.Hide();
            _choiceScreen.Hide();
        }

        public override void Exit() { }

        #endregion
    }
}