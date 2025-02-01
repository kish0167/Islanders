using TMPro;
using UnityEngine;

namespace Islanders.Game.UI.HoveringLabels
{
    public class HoveringLabel : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TMP_Text _label;

        private Transform _targetTransform;

        #endregion

        #region Properties

        public Transform TargetTransform => _targetTransform;

        #endregion

        #region Public methods

        public void Construct(Transform targetTransform, int number)
        {
            _targetTransform = targetTransform;
            _label.text = number == 0 ? "" : $"+{number}";
        }

        #endregion
    }
}