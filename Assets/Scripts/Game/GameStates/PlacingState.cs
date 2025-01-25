using Islanders.Game.Buildings_placing;
using Islanders.Game.LocalCamera;
using Zenject;

namespace Islanders.Game.GameStates
{
    public class PlacingState : GameState
    {
        #region Variables

        private BuildingsPlacer _placer;
        private CameraMovement _cameraMovement;

        #endregion

        #region Setup/Teardown

        [Inject]
        public PlacingState(BuildingsPlacer placer, CameraMovement cameraMovement)
        {
            _placer = placer;
            _cameraMovement = cameraMovement;
        }

        #endregion

        #region Public methods

        public override void Enter()
        {
            _cameraMovement.Enabled = true;
            //_placer.Enabled = true;
        }

        public override void Exit()
        {
            _cameraMovement.Enabled = false;
            _placer.Enabled = false;
        }

        #endregion
    }
}