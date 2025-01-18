using System;
using Islanders.Game.Buildings_placing;
using UnityEngine;

namespace Islanders.ScriptableObjects
{
    [Serializable]
    public class PlaceableObjectArray
    {
        [HideInInspector]
        [SerializeField] private string _name = " ";
        [SerializeField] private PlaceableObject _placeableObject;
        [SerializeField] private uint _quantity = 1;

        public string Name => _name;

        public PlaceableObject PlaceableObject => _placeableObject;

        public uint Quantity => _quantity;

        public void Validate()
        {
            _name = _placeableObject != null ? _placeableObject.name : "Empty!" ;
        }
    }
}