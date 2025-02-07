using Islanders.Game.GameStates;
using Islanders.Game.Pause;
using TMPro;
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
        [SerializeField] private Button _newGameButton;

        private PauseService _pauseService;
        private LocalStateMachine _stateMachine;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(PauseService pauseService, LocalStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _pauseService = pauseService;
            _continueButton.onClick.AddListener(ContinueButtonPressedCallback);
            _exitButton.onClick.AddListener(ExitButtonPressedCallback);
            _newGameButton.onClick.AddListener(NewGameButtonPressedCallback);
        }

        private void NewGameButtonPressedCallback()
        {
            _stateMachine.TransitionTo<GoToNewIslandState>();
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