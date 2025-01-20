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
                if (!Input.GetKeyDown(_controls[bind]))
                {
                    continue;
                }

                OnKeyPressed?.Invoke(bind);
            }
        }

        #endregion
    }
}