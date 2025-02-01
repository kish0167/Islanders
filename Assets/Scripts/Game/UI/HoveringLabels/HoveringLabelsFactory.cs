using Islanders.Game.LocalCamera;
using Lean.Pool;
using UnityEngine;
using Zenject;

namespace Islanders.Game.UI.HoveringLabels
{
    public class HoveringLabelsFactory
    {
        private PlacingScreen _placingScreen;
        
        [Inject]
        public HoveringLabelsFactory(PlacingScreen placingScreen)
        {
            _placingScreen = placingScreen;
        }
        
        public HoveringLabel CreateFromPrefab(HoveringLabel prefab, Transform targetTransform, int number)
        {
            HoveringLabel newLabel = LeanPool.Spawn(prefab, _placingScreen.ContentTransform);
            newLabel.Construct(targetTransform, number);
            return newLabel;
        }
        
        public void Destroy(HoveringLabel instance)
        {
            LeanPool.Despawn(instance);
        }
    }
}