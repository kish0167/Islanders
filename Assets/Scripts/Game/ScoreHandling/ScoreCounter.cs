using UnityEngine;

namespace Islanders.Game.ScoreHandling
{
    public class ScoreCounter : MonoBehaviour
    {
        #region Variables

        [SerializeField] private ObjectType _objectType;

        #endregion

        #region Properties

        public ObjectType Type => _objectType;

        #endregion
    }
}