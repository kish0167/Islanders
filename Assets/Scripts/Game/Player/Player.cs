using System;
using System.Collections.Generic;
using System.Linq;
using Islanders.Game.Buildings_placing;
using Islanders.ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace Islanders.Game.Player
{
    public class Player : MonoBehaviour
    {
        #region Variables

        private Dictionary<PlaceableObject, uint> _placeableObjectInventory = new();

        private BuildingsPlacer _placer;
        private PlaceableObject _selectedObject;
        private int _selectedObjectIndex;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(BuildingsPlacer placer)
        {
            _placer = placer;
            _placer.OnBuildingPlaced += BuildingPlacedCallback;
            Step step = Resources.Load("Script/Step 1") as Step;
            AddToInventory(step?.Choise1);
            _selectedObject = _placeableObjectInventory.Keys.ToList()[0];
            _selectedObjectIndex = 0;
        }

        #endregion

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.F1))
            {
                return;
            }

            _selectedObject = _placeableObjectInventory.Keys.ToList()[_selectedObjectIndex];
            _selectedObjectIndex++;
            
            if (_selectedObjectIndex >= _placeableObjectInventory.Keys.Count)
            {
                _selectedObjectIndex %= _placeableObjectInventory.Keys.Count;
            }
            
            _placer.SetBuilding(_selectedObject);
        }

        #region Public methods

        public void AddToInventory(List<PlaceableObjectArray> incomingObjectArrays)
        {
            foreach (PlaceableObjectArray incomingArray in incomingObjectArrays)
            {
                if (!_placeableObjectInventory.ContainsKey(incomingArray.PlaceableObject))
                {
                    _placeableObjectInventory.Add(incomingArray.PlaceableObject, 0);
                }

                _placeableObjectInventory[incomingArray.PlaceableObject] += incomingArray.Quantity;
            }
        }

        public void Dispose()
        {
            _placer.OnBuildingPlaced -= BuildingPlacedCallback;
        }

        #endregion

        #region Private methods

        private void BuildingPlacedCallback(PlaceableObject building, Vector3 place)
        {
            Debug.LogError(building == _selectedObject);
        }

        #endregion
    }
}