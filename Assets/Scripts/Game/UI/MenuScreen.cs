using Islanders.Game.Pause;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Islanders.Game.UI
{
    public class MenuScreen : MonoBehaviour, IScreen
    {
        #region Variables

        [SerializeField] private GameObject _content;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;

        private PauseService _pauseService;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(PauseService pauseService)
        {
            _pauseService = pauseService;
            _continueButton.onClick.AddListener(ContinueButtonPressedCallback);
            _exitButton.onClick.AddListener(ExitButtonPressedCallback);
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

        private void ContinueButtonPressedCallback()
        {
            _pauseService.Toggle();
        }

        private void ExitButtonPressedCallback()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        #endregion
    }
}