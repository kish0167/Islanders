using Islanders.Game.Buildings_placing;
using UnityEngine;

namespace Islanders.Game.Utility
{
    public class PrefabsProvider : MonoBehaviour
    {
        #region Variables

        [SerializeField] private VisualSphere _transparentSpherePrefab;

        #endregion

        #region Properties

        public VisualSphere TransparentSphere => _transparentSpherePrefab;

        #endregion
    }
}