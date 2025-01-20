using Islanders.Game.ScoreHandling;
using Islanders.Game.Utility;
using Lean.Pool;
using UnityEngine;
using Zenject;

namespace Islanders.Game.Buildings_placing
{
    public class PlaceableObjectFactory
    {
        #region Variables

        private BuildingsPlacer _buildingsPlacer;
        private ScoreTableService _scoreTableService;
        private PrefabsProvider _prefabProvider;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(ScoreTableService tableService, BuildingsPlacer placer, PrefabsProvider provider)
        {
            _scoreTableService = tableService;
            _buildingsPlacer = placer;
            _prefabProvider = provider;
        }

        #endregion

        #region Public methods

        public PlaceableObject CreateFromPrefab(PlaceableObject prefab, Vector3 position)
        {
            PlaceableObject building = LeanPool.Spawn(prefab, position, Quaternion.identity);
            building.gameObject.GetComponent<ScoreCounter>().Construct(_scoreTableService, _buildingsPlacer);
            building.gameObject.layer = LayerMask.NameToLayer(Layers.ActiveBuilding);
            
            VisualSphere sphere = LeanPool.Spawn(_prefabProvider.TransparentSphere, position, Quaternion.identity);
            //sphere.Construct(_buildingsPlacer);
            sphere.transform.SetParent(building.transform);
            sphere.transform.localPosition = Vector3.zero;
            sphere.transform.localScale = Vector3.one * building.gameObject.GetComponent<ScoreCounter>().Radius * 2;
            building.Sphere = sphere;
            
            return building;
        }

        #endregion

        public void Deconstruct(PlaceableObject building)
        {
            LeanPool.Despawn(building.Sphere);
            LeanPool.Despawn(building);
        }
    }
}