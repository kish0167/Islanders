using System.Collections.Generic;
using System.Linq;
using Islanders.Game.Buildings_placing;
using Islanders.Game.LocalCamera;
using Islanders.Game.ScoreHandling;
using Islanders.Game.Utility;
using UnityEngine;
using Zenject;

namespace Islanders.Game.UI.HoveringLabels
{
    public class HoveringLabelsService
    {
        #region Variables

        private readonly HoveringLabelsFactory _factory;
        private readonly HoveringLabel _labelPrefab;
        private readonly Camera _mainCamera;
        private readonly List<HoveringLabel> _hoveringLabels;
        private PlacingScreen _placingScreen;

        #endregion

        #region Setup/Teardown

        [Inject]
        public HoveringLabelsService(PrefabsProvider prefabsProvider, HoveringLabelsFactory factory,
            CameraMovement mainCameraMovement, BuildingsPlacer placer)
        {
            _factory = factory;
            _mainCamera = mainCameraMovement.Camera;
            _labelPrefab = prefabsProvider.HoveringTextLabel;
            _hoveringLabels = new List<HoveringLabel>();
            ScoreCounter.OnPreScoreDrawing += PreScoreDrawingCallback;
            placer.OnBuildingPlaced += BuildingPlacedCallback;
        }

        #endregion

        #region Private methods

        private void BuildingPlacedCallback(PlaceableObject arg1, Vector3 arg2)
        {
            DestroyAllLabels();
        }

        private void DestroyAllLabels()
        {
            foreach (HoveringLabel label in _hoveringLabels)
            {
                _factory.Destroy(label);
            }

            _hoveringLabels.Clear();
        }

        private bool InstanceExists(Transform item)
        {
            foreach (HoveringLabel label in _hoveringLabels)
            {
                if (label.TargetTransform == item)
                {
                    return true;
                }
            }

            return false;
        }

        private void MoveAllLabels()
        {
            foreach (HoveringLabel label in _hoveringLabels)
            {
                label.transform.position = _mainCamera.WorldToScreenPoint(label.TargetTransform.position);
            }
        }

        private void PreScoreDrawingCallback(Dictionary<Transform, int> items)
        {
            foreach (HoveringLabel label in _hoveringLabels)
            {
                if (!items.ContainsKey(label.TargetTransform))
                {
                    _factory.Destroy(label);
                }
            }

            _hoveringLabels.RemoveAll(t => !items.ContainsKey(t.TargetTransform));

            foreach (Transform item in items.Keys.ToList())
            {
                if (!InstanceExists(item))
                {
                    _hoveringLabels.Add(_factory.CreateFromPrefab(_labelPrefab, item, items[item]));
                }
            }

            MoveAllLabels();
        }

        #endregion
    }
}