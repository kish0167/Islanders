using System;
using System.Collections.Generic;
using UnityEngine;

namespace Islanders.Game.ScoreHandling
{
    public class ScoreTable
    {
        #region Variables

        private Dictionary<ObjectType, Dictionary<ObjectType, int>> _table; // сериализовать не получается :С

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            CreateTable();
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

        private void CreateTable()
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

        private void InitializeTable() // le cringe, но ладно
        {
            _table[ObjectType.TestHouse0][ObjectType.TestHouse0] = 3;
            _table[ObjectType.TestHouse0][ObjectType.TestHouse2] = 1;

            _table[ObjectType.TestHouse1][ObjectType.TestHouse0] = 1;
            _table[ObjectType.TestHouse1][ObjectType.TestHouse1] = -1;
            _table[ObjectType.TestHouse1][ObjectType.TestHouse2] = 1;

            _table[ObjectType.TestHouse2][ObjectType.TestHouse1] = 5;
        }

        #endregion
    }
}