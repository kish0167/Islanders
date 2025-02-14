using System;
using System.Collections.Generic;
using System.Linq;
using Islanders.Game.Buildings_placing;
using Islanders.Game.Undo;
using Islanders.ScriptableObjects;
using Islanders.Utils.Log;
using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace Islanders.Game.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        #region Variables

        private readonly Dictionary<PlaceableObject, int> _placeableObjectInventory = new();

        private BuildingsPlacer _placer;
        private PlaceableObject _selectedObject;
        private UndoService _undoService;

        #endregion

        #region Events

        public event Action<Dictionary<PlaceableObject, int>, PlaceableObject> OnInventoryUpdated;

        #endregion

        #region Properties

        public int ItemsCount => _placeableObjectInventory.Count;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(BuildingsPlacer placer, UndoService undoService)
        {
            _placer = placer;
            _placer.OnBuildingPlaced += BuildingPlacedCallback;
            _undoService = undoService;
            _undoService.OnPlacingUndone += PlacingUndoneCallback;
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

            OnInventoryUpdated?.Invoke(_placeableObjectInventory, _selectedObject);
        }

        public void Dispose()
        {
            _placer.OnBuildingPlaced -= BuildingPlacedCallback;
        }

        public void ForceUiUpdate()
        {
            OnInventoryUpdated?.Invoke(_placeableObjectInventory, _selectedObject);
        }

        public void RemoveSelection()
        {
            _selectedObject = null;
            UpdateUi();
        }

        public void SelectBuilding(PlaceableObject selectedBuilding)
        {
            foreach (PlaceableObject placeableObject in _placeableObjectInventory.Keys.ToList())
            {
                if (placeableObject == selectedBuilding)
                {
                    _selectedObject = selectedBuilding;
                    _placer.SetBuilding(selectedBuilding);
                    UpdateUi();
                    return;
                }
            }

            this.Error($"No such building ({selectedBuilding.name}) in inventory");
        }

        #endregion

        #region Private methods

        private void BuildingPlacedCallback(PlaceableObject building, PlaceableObject prefab, Vector3 place)
        {
            _placeableObjectInventory[_selectedObject] -= 1;

            if (_placeableObjectInventory[_selectedObject] <= 0)
            {
                _placeableObjectInventory.Remove(_selectedObject);
                _selectedObject = null;
            }

            UpdateUi();
        }

        private void PlacingUndoneCallback(int arg1, PlaceableObject prefab)
        {
            List<PlaceableObjectArray> listToAdd = new();
            PlaceableObjectArray buildingToAdd = new(prefab, 1);
            listToAdd.Add(buildingToAdd);
            AddToInventory(listToAdd);
        }

        private void UpdateUi()
        {
            OnInventoryUpdated?.Invoke(_placeableObjectInventory, _selectedObject);
        }

        #endregion
    }
}