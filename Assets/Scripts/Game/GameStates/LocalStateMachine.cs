using Zenject;

namespace Islanders.Game.GameStates
{
    public class LocalStateMachine
    {
        #region Variables

        private GameState _currentState;

        #endregion

        #region Setup/Teardown

        [Inject]
        public LocalStateMachine(BootsTrapState bootsTrap)
        {
            _currentState = bootsTrap;
            _currentState.Enter();
        }

        #endregion

        #region Public methods

        public void TransitionTo(GameState newGameState)
        {
            _currentState.Exit();
            _currentState = newGameState;
            _currentState.Enter();
        }

        #endregion
        
    }
}