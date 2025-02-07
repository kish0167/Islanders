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
        public LinearScriptService(PlayerInventory playerInventory, ChoiceScreen choiceScreen,
            LocalStateMachine stateMachine,
            PlayerScore playerScore) :
            base(playerInventory, choiceScreen, stateMachine, playerScore)
        {
            _choiceScreen.OnScreenShown += ScreenShownCallback;
        }

        private void ScreenShownCallback()
        {
            UpdateUi();
        }

        #endregion

        #region Public methods

        public override void ChoiceMadeCallback(int choice)
        {
            switch (choice)
            {
                case 1:
                {
                    _playerInventory.AddToInventory(_script.Steps[_currentStepIndex].Choice1);
                    break;
                }

                case 2:
                {
                    _playerInventory.AddToInventory(_script.Steps[_currentStepIndex].Choice2);
                    break;
                }
                default:
                {
                    this.Error("choice index out of range");
                    break;
                }
            }

            _playerScore.SetNewScoreGoal((int)_script.Steps[_currentStepIndex].ScoreToPass);

            if (_script.Steps.Count - 1 > _currentStepIndex)
            {
                _currentStepIndex++;
            }
            
            _stateMachine.TransitionTo<PlacingState>();
        }

        public override void UpdateUi()
        {
            _choiceScreen.SetButtonsText(_script.Steps[_currentStepIndex].Choice1Caption,
                _script.Steps[_currentStepIndex].Choice2Caption);
        }

        #endregion
    }
}