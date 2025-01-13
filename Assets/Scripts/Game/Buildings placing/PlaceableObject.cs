using System.Collections.Generic;
using Islanders.Game.ScoreHandling;
using UnityEngine;

namespace Islanders.Game.Buildings_placing
{
    [RequireComponent(typeof(CollisionsObserver))]
    [RequireComponent(typeof(ScoreCounter))]
    public class PlaceableObject : MonoBehaviour
    {
        #region Variables

        [Header("Options")]
        [SerializeField] private List<string> _allowedTags;
        [SerializeField] private Vector3 _linecastDirection;

        [Header("Required components")]
        [SerializeField] private CollisionsObserver _observer;

        #endregion

        #region Properties

        public List<string> AllowedTags => _allowedTags;
        public Vector3 LinecastDirection => _linecastDirection;
        public CollisionsObserver Observer => _observer;

        #endregion
    }
}