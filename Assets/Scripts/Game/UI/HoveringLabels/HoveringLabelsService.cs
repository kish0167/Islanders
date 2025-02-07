using System.Collections.Generic;
using System.Linq;
using Islanders.Game.Buildings_placing;
using Islanders.Game.ScoreHandling;
using Islanders.Game.Utility;
using UnityEngine;
using Zenject;

namespace Islanders.Game.UI.HoveringLabels
{
    public class HoveringLabelsService
    {
        #region Variables

        private readonly Vector3 _activeBuildingLabelScale = new(1.4f, 1.4f, 1.4f);
        private readonly HoveringLabelsFactory _factory;
        private readonly List<HoveringLabel> _hoveringLabels;
        private readonly HoveringLabel _labelPrefab;
        private HoveringLabel _activeBuildingLabel;
        private PlacingScreen _placingScreen;

        #endregion

        #region Setup/Teardown

        [Inject]
        public HoveringLabelsService(PrefabsProvider prefabsProvider, HoveringLabelsFactory factory,
            BuildingsPlacer placer)
        {
            _factory = factory;
            _labelPrefab = prefabsProvider.HoveringTextLabel;
            _hoveringLabels = new List<HoveringLabel>();
            ScoreCounter.OnPreScoreDrawing += PreScoreDrawingCallback;
            ScoreCounter.OnPreScoreCalculated += PreScoreCalculatedCallback;
            placer.OnBuildingPlaced += BuildingPlacedCallback;
            placer.OnBuildingHiding += BuildingHidingCallback;
        }

        #endregion

        #region Private methods

        private void BuildingHidingCallback()
        {
            DestroyAllLabels();
        }

        private void BuildingPlacedCallback(PlaceableObject arg1, PlaceableObject arg2, Vector3 arg3)
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

            if (_activeBuildingLabel == null)
            {
                return;
            }

            _activeBuildingLabel.transform.localScale = Vector3.one;
            _factory.Destroy(_activeBuildingLabel);
            _activeBuildingLabel = null;
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

        private void PreScoreCalculatedCallback(Transform activeBuildingTransform, int currentScore)
        {
            if (_activeBuildingLabel == null)
            {
                _activeBuildingLabel = _factory.CreateFromPrefab(_labelPrefab, activeBuildingTransform, currentScore);
                _activeBuildingLabel.transform.localScale = _activeBuildingLabelScale;
            }
            else
            {
                _activeBuildingLabel.Value = currentScore;
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
        }

        #endregion
    }
}