using Lean.Pool;
using UnityEngine;
using Zenject;

namespace Islanders.Game.Buildings_placing
{
    public class VisualSphere : MonoBehaviour
    {
        private BuildingsPlacer _buildingsPlacer;

        public void Construct(BuildingsPlacer placer)
        {
            _buildingsPlacer = placer;
            placer.OnBuildingPlaced += BuildingPlacedCallback;
        }

        private void BuildingPlacedCallback(PlaceableObject arg1, Vector3 arg2)
        {
            transform.parent = null;
            Dispose();
        }

        public void Dispose()
        {
            LeanPool.Despawn(this);
            _buildingsPlacer.OnBuildingPlaced -= BuildingPlacedCallback;
        }
    }
}