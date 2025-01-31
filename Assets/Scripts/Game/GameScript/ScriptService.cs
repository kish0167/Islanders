using Islanders.Game.GameStates;
using Islanders.Game.UI;
using UnityEngine;
using Zenject;

namespace Islanders.Game.GameScript
{
    public abstract class ScriptService : IScriptService
    {
        #region Variables

        protected readonly ChoiceScreen _choiceScreen;
        protected readonly ScriptableObjects.GameScript _script;
        protected readonly LocalStateMachine _stateMachine;
        protected int _currentStepIndex = 0;
        protected Player.Player _player;

        #endregion

        #region Setup/Teardown

        [Inject]
        protected ScriptService(Player.Player player, ChoiceScreen choiceScreen, LocalStateMachine stateMachine)
        {
            _player = player;
            _choiceScreen = choiceScreen;
            _stateMachine = stateMachine;
            _script = Resources.Load<ScriptableObjects.GameScript>("Script/GameScript");
            _choiceScreen.OnChoiceMade += ChoiceMadeCallback;
        }

        #endregion

        #region IScriptService

        public abstract void ProceedToNextStep();

        public abstract void ChoiceMadeCallback(int choice);

        #endregion

        #region Public methods

        public void Deconstruct()
        {
            _choiceScreen.OnChoiceMade -= ChoiceMadeCallback;
        }
        

        #endregion
    }
}