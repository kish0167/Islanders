using Islanders.Game.GameStates;
using Islanders.Game.UI;
using Islanders.ScriptableObjects;
using Islanders.Utils.Log;
using UnityEditor;
using Zenject;

namespace Islanders.Game.GameScript
{
    public class LinearScriptService : ScriptService
    {
        #region Setup/Teardown

        [Inject]
        public LinearScriptService(Player.Player player, ChoiceScreen choiceScreen, LocalStateMachine stateMachine) :
            base(player, choiceScreen, stateMachine)
        {
            this.Log();
        }

        #endregion

        #region Public methods

        public override void ProceedToNextStep()
        {
            _currentStepIndex++;
        }

        public override void ChoiceMadeCallback(int choice)
        {
            switch (choice)
            {
                case 1:
                {
                    _player.AddToInventory(_script.Steps[_currentStepIndex].Choise1);
                    break;
                }
                    
                case 2:
                {
                    _player.AddToInventory(_script.Steps[_currentStepIndex].Choise2);
                    break;
                }
                default:
                {
                    this.Error("choice index out of range");
                    break;
                }
            }
            
            _stateMachine.TransitionTo<PlacingState>();
        }

        #endregion
    }
}