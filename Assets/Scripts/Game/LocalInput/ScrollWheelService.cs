using System;
using Islanders.Game.Buildings_placing;
using Islanders.Game.GameStates;
using Islanders.Game.LocalCamera;
using UnityEngine;
using Zenject;

namespace Islanders.Game.LocalInput
{
    public class ScrollWheelService : MonoBehaviour
    {
        private CameraMovement _cameraMovement;
        private BuildingsPlacer _placer;
        
        [Inject]
        public void Construct( CameraMovement cameraMovement, BuildingsPlacer placer)
        {
            _cameraMovement = cameraMovement;
            _placer = placer;
        }

        private void Update()
        {
            float scrollAmount = Input.GetAxis("Mouse ScrollWheel");
            
            if (scrollAmount == 0)
            {
                return;
            }
            
            if (_placer.IsPlacing)
            {
                _placer.RotateBuilding(scrollAmount);
            }
            else
            {
                _cameraMovement.ChangeFov(scrollAmount);
            }
        }
    }
}