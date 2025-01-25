using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Islanders.Game.LocalInput
{
    public class LocalInputService : MonoBehaviour
    {
        #region Variables

        private Array _allBinds;

        private Dictionary<KeyBind, KeyCode> _controls = new()
        {
            { KeyBind.CameraForward, KeyCode.W },
            { KeyBind.CameraBackward, KeyCode.S },
            { KeyBind.CameraSlideRight, KeyCode.D },
            { KeyBind.CameraSlideLeft, KeyCode.A },
            { KeyBind.CameraYawRight, KeyCode.E },
            { KeyBind.CameraYawLeft, KeyCode.Q },
            { KeyBind.Pause, KeyCode.Escape },
            { KeyBind.HotBar1, KeyCode.Alpha1 },
            { KeyBind.HotBar2, KeyCode.Alpha2 },
            { KeyBind.HotBar3, KeyCode.Alpha3 },
            { KeyBind.HotBar4, KeyCode.Alpha4 },
            { KeyBind.HotBar5, KeyCode.Alpha5 },
        };

        #endregion

        #region Events

        public event Action<KeyBind> OnKeyPressed;
        public event Action<KeyBind> OnKeyDown;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct()
        {
            _allBinds = Enum.GetValues(typeof(KeyBind));
        }

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            foreach (KeyBind bind in _allBinds)
            {
                if (!_controls.TryGetValue(bind, out KeyCode control) || !Input.GetKey(control))
                {
                    continue;
                }

                OnKeyPressed?.Invoke(bind);

                if (Input.GetKeyDown(control))
                {
                    OnKeyDown?.Invoke(bind);
                }
            }
        }

        #endregion
    }
}