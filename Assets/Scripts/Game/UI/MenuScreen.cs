using Islanders.Game.Pause;
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

        private PauseService _pauseService;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(PauseService pauseService)
        {
            _pauseService = pauseService;
        }

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _continueButton.onClick.AddListener(ContinueButtonPressedCallback);
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

        #endregion
    }
}