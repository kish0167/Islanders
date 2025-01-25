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
        [SerializeField] private float _rotateSpeed = 10;
        [Header("Required")]
        [SerializeField] private Transform _cameraTransform;

        #endregion

        #region IInputControllable

        public void DoAction(KeyBind key)
        {
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
                _cameraTransform.Rotate(Vector3.up, _rotateSpeed * 0.01f);
                //_cameraTransform.rotation *= Quaternion.Euler(Vector3.up * (_rotateSpeed * 0.01f));
            }

            if (key == KeyBind.CameraYawLeft)
            {
                _cameraTransform.Rotate(Vector3.up, -_rotateSpeed * 0.01f);
            }
        }

        #endregion
    }
}