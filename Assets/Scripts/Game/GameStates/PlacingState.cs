using Islanders.Game.Buildings_placing;
using Islanders.Game.LocalCamera;
using Islanders.Game.UI;
using Zenject;

namespace Islanders.Game.GameStates
{
    public class PlacingState : GameState
    {
        #region Variables

        private readonly CameraMovement _cameraMovement;

        private readonly BuildingsPlacer _placer;
        private readonly PlacingScreen _placingScreen;

        #endregion

        #region Setup/Teardown

        [Inject]
        public PlacingState(BuildingsPlacer placer, CameraMovement cameraMovement, PlacingScreen placingScreen)
        {
            _placer = placer;
            _cameraMovement = cameraMovement;
            _placingScreen = placingScreen;
        }

        #endregion

        #region Public methods

        public override void Enter()
        {
            _cameraMovement.Enabled = true;
            _placingScreen.Show();
            _placer.Enabled = true;
        }

        public override void Exit()
        {
            _placingScreen.Hide();
            _cameraMovement.Enabled = false;
            _placer.Enabled = false;
        }

        #endregion
    }
}