using Islanders.Game.LocalCamera;
using Islanders.Game.Pause;
using Islanders.Game.UI;
using UnityEngine;
using Zenject;

namespace Islanders.Game.LocalInput
{
    public class KeysActions
    {
        #region Variables

        private readonly LocalInputService _inputService;
        private readonly UiInput _uiInput;
        private readonly CameraMovement _cameraMovement;
        

        #endregion

        #region Setup/Teardown

        [Inject]
        public KeysActions(LocalInputService inputService, UiInput uiInput, CameraMovement cameraMovement)
        {
            _inputService = inputService;
            _uiInput = uiInput;
            _cameraMovement = cameraMovement;
            _inputService.OnKeyPressed += KeyPressedCallback;
            _inputService.OnKeyDown += KeyDownCallback;
        }

        #endregion

        #region Public methods

        public void Destroy()
        {
            _inputService.OnKeyPressed -= KeyPressedCallback;
            _inputService.OnKeyDown -= KeyDownCallback;
        }

        #endregion

        #region Private methods

        private void KeyPressedCallback(KeyBind key)
        {
            
            if (key > KeyBind.CameraStart && key < KeyBind.CameraEnd)
            {
                _cameraMovement.DoAction(key);
                return;
            }
        }
        
        private void KeyDownCallback(KeyBind key)
        {
            if (key > KeyBind.UiStart && key < KeyBind.UiEnd)
            {
                _uiInput.DoAction(key);
                return;
            }
        }

        #endregion
    }
}