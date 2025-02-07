using Islanders.Game.LocalCamera;
using Lean.Pool;
using UnityEngine;
using Zenject;

namespace Islanders.Game.UI.HoveringLabels
{
    public class HoveringLabelsFactory
    {
        #region Variables

        private readonly PlacingScreen _placingScreen;

        private readonly Camera _mainCamera;

        #endregion

        #region Setup/Teardown

        [Inject]
        public HoveringLabelsFactory(PlacingScreen placingScreen, CameraMovement cameraMovement)
        {
            _mainCamera = cameraMovement.Camera;
            _placingScreen = placingScreen;
        }

        #endregion

        #region Public methods

        public HoveringLabel CreateFromPrefab(HoveringLabel prefab, Transform targetTransform, int number)
        {
            HoveringLabel newLabel = LeanPool.Spawn(prefab, _placingScreen.ContentTransform);
            newLabel.Construct(targetTransform, number, _mainCamera);
            return newLabel;
        }

        public void Destroy(HoveringLabel instance)
        {
            LeanPool.Despawn(instance);
        }

        #endregion
    }
}