using System;
using System.Collections.Generic;
using UnityEngine;

namespace Islanders.Game.ScoreHandling
{
    public class ScoreTable : MonoBehaviour
    {
        #region Variables
        
        [SerializeField] private Dictionary<ObjectType, Dictionary<ObjectType, int>> _table;
        
        #endregion

        #region Unity lifecycle

        private void Start()
        {
            InitializeTable();
        }

        #endregion

        #region Public methods

        public Dictionary<ObjectType, int> GetDictionaryForType(ObjectType type)
        {
            return _table[type];
        }

        #endregion

        #region Private methods

        private void InitializeTable()
        {
            foreach (ObjectType objectTypeI in Enum.GetValues(typeof(ObjectType)))
            {
                Dictionary<ObjectType, int> row = new();

                foreach (ObjectType objectTypeJ in Enum.GetValues(typeof(ObjectType)))
                {
                    row.Add(objectTypeJ, 0);
                }

                _table.Add(objectTypeI, row);
            }
        }

        #endregion
    }
}