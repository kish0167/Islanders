using Islanders.Game.GameStates;
using Zenject;

namespace Islanders.Game.Pause
{
    public class PauseService
    {
        #region Variables

        private readonly LocalStateMachine _stateMachine;

        #endregion

        #region Properties

        public bool IsPaused => _stateMachine.Is<MenuState>();

        #endregion

        #region Setup/Teardown

        [Inject]
        public PauseService(LocalStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        #endregion

        #region Public methods

        public void ForcePause()
        {
            _stateMachine.TransitionTo<MenuState>();
        }

        public void Toggle()
        {
            if (_stateMachine.Is<MenuState>())
            {
                _stateMachine.TransitionTo<PlacingState>();
            }
            else if (_stateMachine.Is<PlacingState>())
            {
                _stateMachine.TransitionTo<MenuState>();
            }
            else if (_stateMachine.Is<ChoosingState>())
            {
                _stateMachine.TransitionTo<PlacingState>();
            }
        }

        #endregion
    }
}