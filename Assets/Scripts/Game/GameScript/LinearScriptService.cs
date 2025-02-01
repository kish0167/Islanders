using Islanders.Game.GameStates;
using Islanders.Game.Player;
using Islanders.Game.UI;
using Islanders.Utils.Log;
using Zenject;

namespace Islanders.Game.GameScript
{
    public class LinearScriptService : ScriptService
    {
        #region Setup/Teardown

        [Inject]
        public LinearScriptService(Player.PlayerInventory playerInventory, ChoiceScreen choiceScreen, LocalStateMachine stateMachine,
            PlayerScore playerScore) :
            base(playerInventory, choiceScreen, stateMachine, playerScore) { }

        #endregion

        #region Public methods

        public override void ChoiceMadeCallback(int choice)
        {
            switch (choice)
            {
                case 1:
                {
                    _playerInventory.AddToInventory(_script.Steps[_currentStepIndex].Choise1);
                    break;
                }

                case 2:
                {
                    _playerInventory.AddToInventory(_script.Steps[_currentStepIndex].Choise2);
                    break;
                }
                default:
                {
                    this.Error("choice index out of range");
                    break;
                }
            }
            
            _playerScore.SetNewScoreGoal((int)_script.Steps[_currentStepIndex].ScoreToPass);
            _stateMachine.TransitionTo<PlacingState>();
            _currentStepIndex++;
        }

        #endregion
    }
}