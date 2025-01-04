using UnityEngine;

namespace Islanders.Game.Buildings_placing
{
    [RequireComponent(typeof(CollisionsObserver))]
    public class PlaceableObject : MonoBehaviour
    {
        [SerializeField] private CollisionsObserver _observer;
        public CollisionsObserver Observer => _observer;
    }
}