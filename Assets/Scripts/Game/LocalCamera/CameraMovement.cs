using Islanders.Game.LocalInput;
using Islanders.Game.Utility;
using UnityEngine;

namespace Islanders.Game.LocalCamera
{
    public class CameraMovement : MonoBehaviour, IInputControllable
    {
        #region Variables

        [Header("Options")]
        [SerializeField] private float _slideSpeed = 10;
        [SerializeField] private float _rotateSpeed = 40;
        [Header("Required")]
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Camera _camera;

        #endregion

        #region Properties

        public bool Enabled { get; set; } = false;

        #endregion

        #region IInputControllable

        private void Update()
        {
            if (!Enabled)
            {
                return;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            _camera.fieldOfView += scroll *= -20;  // TODO: REDO
        }

        public void DoAction(KeyBind key)
        {
            if (!Enabled)
            {
                return;
            }

            if (key == KeyBind.CameraForward)
            {
                _cameraTransform.position += _cameraTransform.forward * (_slideSpeed * 0.01f);
            }

            if (key == KeyBind.CameraBackward)
            {
                _cameraTransform.position -= _cameraTransform.forward * (_slideSpeed * 0.01f);
            }

            if (key == KeyBind.CameraSlideRight)
            {
                _cameraTransform.position += _cameraTransform.right * (_slideSpeed * 0.01f);
            }

            if (key == KeyBind.CameraSlideLeft)
            {
                _cameraTransform.position -= _cameraTransform.right * (_slideSpeed * 0.01f);
            }

            if (key == KeyBind.CameraYawRight)
            {
                _cameraTransform.Rotate(Vector3.up, -_rotateSpeed * 0.01f);
            }

            if (key == KeyBind.CameraYawLeft)
            {
                _cameraTransform.Rotate(Vector3.up, _rotateSpeed * 0.01f);
            }
        }

        #endregion
    }
}