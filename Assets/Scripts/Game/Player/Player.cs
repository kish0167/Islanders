using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Islanders.Game.Buildings_placing;
using Islanders.ScriptableObjects;
using Islanders.Utils.Log;
using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace Islanders.Game.Player
{
    public class Player : MonoBehaviour
    {
        #region Variables

        private readonly Dictionary<PlaceableObject, int> _placeableObjectInventory = new();

        private BuildingsPlacer _placer;
        private PlaceableObject _selectedObject;
        // private int _selectedObjectIndex;

        #endregion

        #region Events

        public event Action<Dictionary<PlaceableObject, int>, PlaceableObject> OnInventoryUpdated;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(BuildingsPlacer placer)
        {
            _placer = placer;
            _placer.OnBuildingPlaced += BuildingPlacedCallback;
        }

        #endregion

        #region Unity lifecycle
        

        private void Start()
        {
            Step step = Resources.Load("Script/Step 1") as Step;
            AddToInventory(step?.Choise1);
            _selectedObject = _placeableObjectInventory.Keys.ToList()[0];
            //_selectedObjectIndex = 0;
        }

        #endregion

        #region Public methods

        public void AddToInventory(List<PlaceableObjectArray> incomingObjectArrays)
        {
            foreach (PlaceableObjectArray incomingArray in incomingObjectArrays)
            {
                if (!_placeableObjectInventory.ContainsKey(incomingArray.PlaceableObject))
                {
                    _placeableObjectInventory.Add(incomingArray.PlaceableObject, 0);
                }

                _placeableObjectInventory[incomingArray.PlaceableObject] += (int)incomingArray.Quantity;
            }
            
            this.Log("Pack added to inventory");
            LogInventoryContent();
            
            OnInventoryUpdated?.Invoke(_placeableObjectInventory, _selectedObject);
        }

        public void Dispose()
        {
            _placer.OnBuildingPlaced -= BuildingPlacedCallback;
        }

        #endregion

        #region Private methods

        private void BuildingPlacedCallback(PlaceableObject building, Vector3 place)
        {
            _placeableObjectInventory[_selectedObject] -= 1;

            if (_placeableObjectInventory[_selectedObject] > 0)
            {
                OnInventoryUpdated?.Invoke(_placeableObjectInventory, _selectedObject);
                return;
            }

            _selectedObject = null;
            _placer.Disable();
            
            LogInventoryContent();
            
            OnInventoryUpdated?.Invoke(_placeableObjectInventory, _selectedObject);
        }

        private void LogInventoryContent()
        {
            string text = "Inventory contains:\n";
            
            foreach (PlaceableObject placeableObject in _placeableObjectInventory.Keys.ToList())
            {
                text += placeableObject.name;
                text += "\t";
                text += _placeableObjectInventory[placeableObject];
                text += "\n";
            }
            
            this.Log(text);
        }

        #endregion
    }
}