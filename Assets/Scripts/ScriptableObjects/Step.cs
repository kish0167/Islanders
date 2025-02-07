using System.Collections.Generic;
using UnityEngine;

namespace Islanders.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Step", menuName = "Script/Step")]
    public class Step : ScriptableObject
    {
        #region Variables

        [SerializeField] private string _choice1Caption;
        [SerializeField] private List<PlaceableObjectArray> _choice1;
        [SerializeField] private string _choice2Caption;
        [SerializeField] private List<PlaceableObjectArray> _choice2;
        [SerializeField] private uint _scoreToPass = 1;

        #endregion

        #region Properties

        public List<PlaceableObjectArray> Choice1 => _choice1;

        public string Choice1Caption => _choice1Caption;

        public List<PlaceableObjectArray> Choice2 => _choice2;

        public string Choice2Caption => _choice2Caption;

        public uint ScoreToPass => _scoreToPass;

        #endregion

        #region Unity lifecycle

        private void OnValidate()
        {
            foreach (PlaceableObjectArray placeableObjectArray in _choice1)
            {
                placeableObjectArray.Validate();
            }

            foreach (PlaceableObjectArray placeableObjectArray in _choice2)
            {
                placeableObjectArray.Validate();
            }
        }

        #endregion
    }
}