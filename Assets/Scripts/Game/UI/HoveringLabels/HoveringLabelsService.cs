using System.Collections.Generic;
using System.Linq;
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
        private List<HoveringLabel> _hoveringLabels;

        private HoveringLabel _labelPrefab;
        private PlacingScreen _placingScreen;

        #endregion

        #region Setup/Teardown

        [Inject]
        public HoveringLabelsService(PrefabsProvider prefabsProvider, HoveringLabelsFactory factory)
        {
            _factory = factory;
            _labelPrefab = prefabsProvider.HoveringTextLabel;
            ScoreCounter.OnPreScoreDrawing += PreScoreDrawingCallback;
        }

        #endregion

        #region Private methods

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
                    _hoveringLabels.Add(_factory.CreateFromPrefab(_labelPrefab, item));
                }
            }
        }

        #endregion
    }
}