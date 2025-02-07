using Islanders.Game.Buildings_placing;
using Islanders.Game.UI.Hotbar;
using Islanders.Game.UI.HoveringLabels;
using UnityEngine;

namespace Islanders.Game.Utility
{
    public class PrefabsProvider : MonoBehaviour
    {
        #region Variables

        [SerializeField] private VisualSphere _transparentSpherePrefab;
        [SerializeField] private HotBarButton _hotBarButton;
        [SerializeField] private Material _prohibitingMaterial;
        [SerializeField] private HoveringLabel _hoveringTextLabel;

        #endregion

        #region Properties

        public HotBarButton HotBarButton => _hotBarButton;
        public HoveringLabel HoveringTextLabel => _hoveringTextLabel;

        public Material ProhibitingMaterial => _prohibitingMaterial;

        public VisualSphere TransparentSphere => _transparentSpherePrefab;

        #endregion
    }
}