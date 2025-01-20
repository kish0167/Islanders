using Islanders.Game.GameStates;
using UnityEngine;
using Zenject;

namespace Islanders.Game.Pause
{
    public class PauseService
    {
        #region Variables

        private readonly MenuState _menuState;
        private readonly PlacingState _placingState;

        private readonly LocalStateMachine _stateMachine;
        private bool _isPaused;

        #endregion

        #region Setup/Teardown

        [Inject]
        public PauseService(LocalStateMachine stateMachine, PlacingState placingState, MenuState menuState)
        {
            _stateMachine = stateMachine;
            _placingState = placingState;
            _menuState = menuState;
        }

        #endregion

        #region Public methods

        public void Toggle()
        {
            if (_isPaused)
            {
                _stateMachine.TransitionTo(_placingState);
                Debug.Log("pause off");
            }
            else
            {
                _stateMachine.TransitionTo(_menuState);
                Debug.Log("pause on");
            }

            _isPaused = !_isPaused;
        }

        #endregion
    }
}