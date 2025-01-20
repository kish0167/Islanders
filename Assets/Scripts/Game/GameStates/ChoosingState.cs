using Islanders.Game.UI;
using Zenject;

namespace Islanders.Game.GameStates
{
    public class ChoosingState : GameState
    {
        private ChoiceScreen _choiceScreen;
        
        [Inject]
        public ChoosingState(ChoiceScreen choiceScreen)
        {
            _choiceScreen = choiceScreen;
        }

        public override void Enter()
        {
            _choiceScreen.Show();
        }

        public override void Exit()
        {
            _choiceScreen.Hide();
        }
    }
}