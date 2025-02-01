using TMPro;
using UnityEngine;

namespace Islanders.Game.UI.HoveringLabels
{
    public class HoveringLabel : MonoBehaviour
    {
        #region Variables

        private static readonly Vector3 VerticalLabelsCorrection = new(0, 1, 0);

        [SerializeField] private TMP_Text _label;
        private Camera _mainCamera;

        private Transform _targetTransform;
        private int _value;

        #endregion

        #region Properties

        public Transform TargetTransform => _targetTransform;

        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateLabel();
            }
        }

        #endregion

        #region Unity lifecycle

        private void LateUpdate()
        {
            Move();
        }

        #endregion

        #region Public methods

        public void Construct(Transform targetTransform, int number, Camera mainCamera)
        {
            _mainCamera = mainCamera;
            _targetTransform = targetTransform;
            Value = number;
        }

        #endregion

        #region Private methods

        private void Move()
        {
            transform.position = _mainCamera.WorldToScreenPoint(TargetTransform.position + VerticalLabelsCorrection);
        }

        private void UpdateLabel()
        {
            _label.text = _value == 0 ? "" : $"+{_value}";
        }

        #endregion
    }
}