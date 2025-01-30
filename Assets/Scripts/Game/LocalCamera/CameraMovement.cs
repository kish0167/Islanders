using System;
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
        [SerializeField] private float _fovChangeSpeed = 20;
        [SerializeField] private float _maxFov = 30;
        [SerializeField] private float _minFov = 7;
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
            
            Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
        

        public void DoAction(KeyBind key)
        {
            if (!Enabled)
            {
                return;
            }

            if (key == KeyBind.CameraForward)
            {
                _cameraTransform.position += _cameraTransform.forward * (_slideSpeed * Time.deltaTime);
            }

            if (key == KeyBind.CameraBackward)
            {
                _cameraTransform.position -= _cameraTransform.forward * (_slideSpeed * Time.deltaTime);
            }

            if (key == KeyBind.CameraSlideRight)
            {
                _cameraTransform.position += _cameraTransform.right * (_slideSpeed * Time.deltaTime);
            }

            if (key == KeyBind.CameraSlideLeft)
            {
                _cameraTransform.position -= _cameraTransform.right * (_slideSpeed * Time.deltaTime);
            }

            if (key == KeyBind.CameraYawRight)
            {
                _cameraTransform.Rotate(Vector3.up, -_rotateSpeed * Time.deltaTime);
            }

            if (key == KeyBind.CameraYawLeft)
            {
                _cameraTransform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
            }
        }

        private void Zoom(float wheelScroll)
        {
            float newFov = _camera.fieldOfView;
            newFov -= wheelScroll * _fovChangeSpeed * Time.deltaTime * 60;
            newFov = Math.Clamp(newFov, _minFov, _maxFov);
            _camera.fieldOfView = newFov;
        }

        #endregion
    }
}