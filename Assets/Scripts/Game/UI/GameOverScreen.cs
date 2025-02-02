using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Islanders.Game.UI
{
    public class GameOverScreen : MonoBehaviour, IScreen
    {
        #region Variables

        [SerializeField] private GameObject _content;
        [SerializeField] private Button _nextButton;

        public event Action OnNextButtonPressed;
        
        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct()
        {
            _nextButton.onClick.AddListener(NextButtonPressedCallback);
        }

        private void NextButtonPressedCallback()
        {
            OnNextButtonPressed?.Invoke();
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
    }
}