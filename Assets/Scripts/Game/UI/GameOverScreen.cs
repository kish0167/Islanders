using System;
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
        [SerializeField] private Button _undoButton;

        #endregion

        #region Events

        public event Action OnNextButtonPressed;
        public event Action OnUndoButtonPressed;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct()
        {
            _nextButton.onClick.AddListener(NextButtonPressedCallback);
            _nextButton.onClick.AddListener(UndoButtonPressedCallback);
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

        private void NextButtonPressedCallback()
        {
            OnNextButtonPressed?.Invoke();
        }

        private void UndoButtonPressedCallback()
        {
            OnUndoButtonPressed?.Invoke();
        }

        #endregion
    }
}