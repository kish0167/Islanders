using Islanders.Game.UI;
using Zenject;

namespace Islanders.Game.GameStates
{
    public class ChoosingState : GameState
    {
        #region Variables

        private readonly ChoiceScreen _choiceScreen;

        #endregion

        #region Setup/Teardown

        [Inject]
        public ChoosingState(ChoiceScreen choiceScreen)
        {
            _choiceScreen = choiceScreen;
        }

        #endregion

        #region Public methods

        public override void Enter()
        {
            _choiceScreen.Show();
        }

        public override void Exit()
        {
            _choiceScreen.Hide();
        }

        #endregion
    }
}