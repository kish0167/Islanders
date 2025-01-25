using Islanders.Game.Buildings_placing;
using Islanders.Game.UI.Hotbar;
using UnityEngine;

namespace Islanders.Game.Utility
{
    public class PrefabsProvider : MonoBehaviour
    {
        #region Variables

        [SerializeField] private VisualSphere _transparentSpherePrefab;
        [SerializeField] private HotBarButton _hotBarButton;

        #endregion

        #region Properties

        public HotBarButton HotBarButton => _hotBarButton;

        public VisualSphere TransparentSphere => _transparentSpherePrefab;

        #endregion
    }
}