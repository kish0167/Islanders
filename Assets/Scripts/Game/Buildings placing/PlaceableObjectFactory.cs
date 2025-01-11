using Islanders.Game.ScoreHandling;
using Lean.Pool;
using UnityEngine;
using Zenject;

namespace Islanders.Game.Buildings_placing
{
    public class PlaceableObjectFactory
    {
        #region Variables

        private BuildingsPlacer _buildingsPlacer;

        private ScoreService _scoreService;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(ScoreService service, BuildingsPlacer placer)
        {
            _scoreService = service;
            _buildingsPlacer = placer;
        }

        #endregion

        #region Public methods

        public PlaceableObject CreateFromPrefab(PlaceableObject prefab, Vector3 position)
        {
            PlaceableObject building = LeanPool.Spawn(prefab, position, Quaternion.identity);
            building.gameObject.GetComponent<ScoreCounter>().Construct(_scoreService, _buildingsPlacer);
            return building;
        }

        #endregion
    }
}