using Islanders.Game.LocalCamera;
using Lean.Pool;
using UnityEngine;
using Zenject;

namespace Islanders.Game.UI.HoveringLabels
{
    public class HoveringLabelsFactory
    {
        private PlacingScreen _placingScreen;
        private Transform _mainCameraTransform;
        
        [Inject]
        public HoveringLabelsFactory(PlacingScreen placingScreen, CameraMovement cameraMovement)
        {
            _placingScreen = placingScreen;
            _mainCameraTransform = cameraMovement.Camera.transform;
        }
        
        public HoveringLabel CreateFromPrefab(HoveringLabel prefab, Transform targetTransform)
        {
            HoveringLabel newLabel = LeanPool.Spawn(prefab, _placingScreen.ContentTransform);
            newLabel.Construct(_mainCameraTransform, targetTransform);
            return newLabel;
        }
        
        public void Destroy(HoveringLabel instance)
        {
            
        }
    }
}