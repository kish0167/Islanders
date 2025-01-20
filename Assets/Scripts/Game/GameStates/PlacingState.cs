using Islanders.Game.Buildings_placing;
using Zenject;

namespace Islanders.Game.GameStates
{
    public class PlacingState : GameState
    {
        #region Variables

        private BuildingsPlacer _placer;

        #endregion

        #region Setup/Teardown

        [Inject]
        public PlacingState(BuildingsPlacer placer)
        {
            _placer = placer;
        }

        #endregion

        #region Public methods

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            _placer.Disable();
        }

        #endregion
    }
}