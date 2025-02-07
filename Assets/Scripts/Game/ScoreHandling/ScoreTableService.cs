using System;
using System.Collections.Generic;

namespace Islanders.Game.ScoreHandling
{
    public class ScoreTableService
    {
        #region Variables

        private Dictionary<ObjectType, Dictionary<ObjectType, int>> _table; // сериализовать не получается :С
        private Dictionary<ObjectType, int> _ownScores;

        #endregion

        #region Setup/Teardown

        public ScoreTableService()
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

        public int GetOwnScore(ObjectType type)
        {
            return _ownScores[type];
        }

        #endregion

        #region Private methods

        private void CreateTable()
        {
            _table = new Dictionary<ObjectType, Dictionary<ObjectType, int>>();
            _ownScores = new();

            foreach (ObjectType objectTypeI in Enum.GetValues(typeof(ObjectType)))
            {
                Dictionary<ObjectType, int> row = new();

                foreach (ObjectType objectTypeJ in Enum.GetValues(typeof(ObjectType)))
                {
                    row.Add(objectTypeJ, 0);
                }

                _table.Add(objectTypeI, row);
                _ownScores.Add(objectTypeI, 0);
            }
        }

        private void InitializeTable() // le cringe, но ладно
        {
            _table[ObjectType.TestHouse0][ObjectType.TestHouse0] = 3;
            _table[ObjectType.TestHouse0][ObjectType.TestHouse2] = 1;

            _table[ObjectType.TestHouse1][ObjectType.TestHouse0] = 1;
            _table[ObjectType.TestHouse1][ObjectType.TestHouse1] = -1;
            _table[ObjectType.TestHouse1][ObjectType.TestHouse2] = 1;

            _table[ObjectType.TestHouse2][ObjectType.TestHouse2] = 5;

            _ownScores[ObjectType.TowerBlueSmall] = 0;
            _table[ObjectType.TowerBlueSmall][ObjectType.TowerBlueSmall] = -5;
            _table[ObjectType.TowerBlueSmall][ObjectType.TowerBlue] = -7;
            _table[ObjectType.TowerBlueSmall][ObjectType.PoorHouse] = 1;
            _table[ObjectType.TowerBlueSmall][ObjectType.RichHouse] = 2;
            
            _ownScores[ObjectType.TowerBlue] = 0;
            _table[ObjectType.TowerBlue][ObjectType.TowerBlue] = -10;
            _table[ObjectType.TowerBlue][ObjectType.TowerBlueSmall] = -5;
            _table[ObjectType.TowerBlue][ObjectType.PoorHouse] = 2;
            _table[ObjectType.TowerBlue][ObjectType.RichHouse] = 2;
            
            _ownScores[ObjectType.TowerRedSmall] = 0;
            _table[ObjectType.TowerRedSmall][ObjectType.TowerRedSmall] = -5;
            _table[ObjectType.TowerRedSmall][ObjectType.TowerRed] = -7;
            _table[ObjectType.TowerRedSmall][ObjectType.PoorHouse] = 1;
            _table[ObjectType.TowerRedSmall][ObjectType.RichHouse] = 2;
            
            _ownScores[ObjectType.TowerRed] = 0;
            _table[ObjectType.TowerRed][ObjectType.TowerRed] = -10;
            _table[ObjectType.TowerRed][ObjectType.TowerRedSmall] = -5;
            _table[ObjectType.TowerRed][ObjectType.PoorHouse] = 2;
            _table[ObjectType.TowerRed][ObjectType.RichHouse] = 2;
            
            _ownScores[ObjectType.Blacksmith] = 0;
            _table[ObjectType.Blacksmith][ObjectType.Blacksmith] = -5;
            _table[ObjectType.Blacksmith][ObjectType.TowerBlue] = 5;
            _table[ObjectType.Blacksmith][ObjectType.TowerRed] = 5;
            _table[ObjectType.Blacksmith][ObjectType.PoorHouse] = 3;
            _table[ObjectType.Blacksmith][ObjectType.RichHouse] = 7;
            
            _ownScores[ObjectType.Castle] = 15;
            _table[ObjectType.Castle][ObjectType.Castle] = -30;
            _table[ObjectType.Castle][ObjectType.Church] = 20;
            _table[ObjectType.Castle][ObjectType.RichHouse] = 5;
            _table[ObjectType.Castle][ObjectType.PoorHouse] = 3;
            
            _ownScores[ObjectType.PoorHouse] = 0;
            _table[ObjectType.PoorHouse][ObjectType.PoorHouse] = 2;
            _table[ObjectType.PoorHouse][ObjectType.Well] = 7;
            _table[ObjectType.PoorHouse][ObjectType.Castle] = 8;
            _table[ObjectType.PoorHouse][ObjectType.Windmill] = 3;
            _table[ObjectType.PoorHouse][ObjectType.Blacksmith] = 3;
            _table[ObjectType.PoorHouse][ObjectType.Watermill] = 1;
            
            _ownScores[ObjectType.RichHouse] = 0;
            _table[ObjectType.RichHouse][ObjectType.RichHouse] = 4;
            _table[ObjectType.RichHouse][ObjectType.PoorHouse] = -1;
            _table[ObjectType.RichHouse][ObjectType.Tavern] = 3;
            _table[ObjectType.RichHouse][ObjectType.Mine] = -2;
            
            _ownScores[ObjectType.Church] = -10;
            _table[ObjectType.Church][ObjectType.Church] = -31;
            _table[ObjectType.Church][ObjectType.Tavern] = -15;
            _table[ObjectType.Church][ObjectType.PoorHouse] = 5;
            _table[ObjectType.Church][ObjectType.RichHouse] = 3;
            _table[ObjectType.Church][ObjectType.Mansion] = 1;
            _table[ObjectType.Church][ObjectType.Castle] = 25;
            
            _ownScores[ObjectType.Windmill] = 5;
            _table[ObjectType.Windmill][ObjectType.Windmill] = -8;
            _table[ObjectType.Windmill][ObjectType.PoorHouse] = 2;
            _table[ObjectType.Windmill][ObjectType.RichHouse] = 1;
            _table[ObjectType.Windmill][ObjectType.Mine] = -5;
            _table[ObjectType.Windmill][ObjectType.Tavern] = 10;
            
            _ownScores[ObjectType.Well] = -2;
            _table[ObjectType.Well][ObjectType.Well] = -17;
            _table[ObjectType.Well][ObjectType.PoorHouse] = 2;
            _table[ObjectType.Well][ObjectType.RichHouse] = 3;
            _table[ObjectType.Well][ObjectType.Tavern] = 10;
            _table[ObjectType.Well][ObjectType.Watermill] = -5;
            _table[ObjectType.Well][ObjectType.Mansion] = 10;
            
            _ownScores[ObjectType.Tavern] = 0;
            _table[ObjectType.Tavern][ObjectType.Tavern] = -24;
            _table[ObjectType.Tavern][ObjectType.PoorHouse] = 5;
            _table[ObjectType.Tavern][ObjectType.RichHouse] = 7;
            
            _ownScores[ObjectType.Mansion] = 0;
            _table[ObjectType.Mansion][ObjectType.Mansion] = -16;
            _table[ObjectType.Mansion][ObjectType.Church] = 5;
            _table[ObjectType.Mansion][ObjectType.PoorHouse] = -3;
            _table[ObjectType.Mansion][ObjectType.RichHouse] = -1;
            _table[ObjectType.Mansion][ObjectType.Well] = 5;
            _table[ObjectType.Mansion][ObjectType.Tavern] = 10;
            
            _ownScores[ObjectType.Watermill] = 0;
            _table[ObjectType.Watermill][ObjectType.Watermill] = 1;
            _table[ObjectType.Watermill][ObjectType.Blacksmith] = 13;
            _table[ObjectType.Watermill][ObjectType.Mine] = 17;
            _table[ObjectType.Watermill][ObjectType.Church] = -29;
            
            _ownScores[ObjectType.Mine] = 20;
            _table[ObjectType.Mine][ObjectType.Mine] = -5;
            _table[ObjectType.Mine][ObjectType.Watermill] = 10;
            _table[ObjectType.Mine][ObjectType.Tavern] = 7;
            _table[ObjectType.Mine][ObjectType.PoorHouse] = 3;
        }

        #endregion
    }
}