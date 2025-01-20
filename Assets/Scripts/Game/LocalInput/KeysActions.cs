using Islanders.Game.GameStates;
using Islanders.Game.Pause;
using Islanders.Game.UI;
using UnityEngine.UIElements;
using Zenject;

namespace Islanders.Game.LocalInput
{
    public class KeysActions
    {
        #region Variables

        private readonly LocalInputService _inputService;
        private readonly PauseService _pauseService;

        #endregion

        #region Setup/Teardown

        [Inject]
        public KeysActions(LocalInputService inputService, PauseService pauseService)
        {
            _inputService = inputService;
            _pauseService = pauseService;
            _inputService.OnKeyPressed += KeyPressedCallback;
        }

        #endregion

        #region Public methods

        public void Destroy()
        {
            _inputService.OnKeyPressed -= KeyPressedCallback;
        }

        #endregion

        #region Private methods

        private void KeyPressedCallback(KeyBind key)
        {
            if (key == KeyBind.Pause)
            {
                _pauseService.Toggle();
            }
        }
        
        

        #endregion
    }
}