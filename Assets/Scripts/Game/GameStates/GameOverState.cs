using Islanders.Game.LocalCamera;
using Islanders.Game.UI;
using Zenject;

namespace Islanders.Game.GameStates
{
    public class GameOverState : GameState
    {
        #region Variables

        private GameOverScreen _gameOverScreen;
        private CameraMovement _cameraMovement;

        #endregion

        #region Setup/Teardown

        [Inject]
        public GameOverState(GameOverScreen gameOverScreen, CameraMovement cameraMovement)
        {
            _gameOverScreen = gameOverScreen;
            _cameraMovement = cameraMovement;
        }

        #endregion

        #region Public methods

        public override void Enter()
        {
            _gameOverScreen.Show();
            _cameraMovement.Enabled = true;
        }

        public override void Exit()
        {
            _gameOverScreen.Hide();
            _cameraMovement.Enabled = false;
        }

        #endregion
    }
}