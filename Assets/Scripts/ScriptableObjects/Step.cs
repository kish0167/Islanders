using System.Collections.Generic;
using Islanders.Game.Player;
using UnityEngine;

namespace Islanders.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Step", menuName = "Script/Step")]
    public class Step : ScriptableObject
    {
        #region Variables

        [SerializeField] private List<PlaceableObjectArray> _choise1;
        [SerializeField] private List<PlaceableObjectArray> _choise2;
        [SerializeField] private uint _scoreToPass = 1;

        #endregion

        #region Unity lifecycle

        private void OnValidate()
        {
            foreach (PlaceableObjectArray placeableObjectArray in _choise1)
            {
                placeableObjectArray.Validate();
            }

            foreach (PlaceableObjectArray placeableObjectArray in _choise2)
            {
                placeableObjectArray.Validate();
            }
        }

        #endregion
    }
}