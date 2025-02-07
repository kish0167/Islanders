using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Islanders.Game.UI
{
    public class ChoiceScreen : MonoBehaviour, IScreen
    {
        #region Variables

        [SerializeField] private GameObject _content;
        [SerializeField] private Button _1ChoiceButton;
        [SerializeField] private Button _2ChoiceButton;

        #endregion

        #region Events

        public event Action<int> OnChoiceMade;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct()
        {
            _1ChoiceButton.onClick.AddListener(Button1PressedCallback);
            _2ChoiceButton.onClick.AddListener(Button2PressedCallback);
        }

        #endregion

        #region IScreen

        public void Hide()
        {
            _content.SetActive(false);
        }

        public void Show()
        {
            _content.SetActive(true);
        }

        #endregion

        #region Private methods

        private void Button1PressedCallback()
        {
            OnChoiceMade?.Invoke(1);
        }

        private void Button2PressedCallback()
        {
            OnChoiceMade?.Invoke(2);
        }

        #endregion
    }
}