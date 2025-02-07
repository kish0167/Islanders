using System;
using TMPro;
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
        [SerializeField] private TMP_Text _button1Caption;
        [SerializeField] private TMP_Text _button2Caption;
        

        #endregion

        #region Events

        public event Action<int> OnChoiceMade;
        public event Action OnScreenShown;

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
            OnScreenShown?.Invoke();
        }

        #endregion

        #region Public methods

        public void SetButtonsText(string text1, string text2)
        {
            _button1Caption.text = text1;
            _button2Caption.text = text2;
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