using TMPro;
using UnityEngine;

namespace Islanders.Game.UI.HoveringLabels
{
    public class HoveringLabel : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TMP_Text _label;

        private Transform _cameraTransform;
        private Transform _targetTransform;

        #endregion

        #region Properties

        public Transform TargetTransform => _targetTransform;

        #endregion

        #region Public methods

        public void Construct(Transform mainCameraTransform, Transform targetTransform)
        {
            _cameraTransform = mainCameraTransform;
            _targetTransform = targetTransform;
        }

        #endregion
    }
}