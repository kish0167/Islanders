using System.Collections.Generic;
using Islanders.Game.Buildings_placing;
using Islanders.ScriptableObjects;
using Zenject;

namespace Islanders.Game.Player
{
    public class Player
    {
        #region Variables

        private Dictionary<PlaceableObject, uint> _placeableObjectInventory;

        private BuildingsPlacer _placer;
        private int _selectedObjectIndex = -1;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(BuildingsPlacer placer)
        {
            _placer = placer;
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

                _placeableObjectInventory[incomingArray.PlaceableObject] += incomingArray.Quantity;
            }
        }
        
        

        #endregion
    }
}