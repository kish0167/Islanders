using Islanders.Game.Buildings_placing;
using UnityEngine;
using UnityEngine.UI;

namespace Islanders.Game.UI.Hotbar
{
    public class HotBarButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private BuildingsPlacer _placer;

        public PlaceableObject Prefab { get; set; }

        public void Construct(BuildingsPlacer placer)
        {
            _placer = placer;
        }
        
    }
}