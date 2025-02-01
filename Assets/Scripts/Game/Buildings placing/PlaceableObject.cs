using System;
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
        private Material _defaultMaterial;
        private Material _prohibitingMaterial;

        public Material ProhibitingMaterial
        {
            get => _prohibitingMaterial;
            set => _prohibitingMaterial = value;
        }

        private MeshRenderer _meshRenderer;

        #endregion

        #region Properties

        public List<string> AllowedTags => _allowedTags;
        public Vector3 LinecastDirection => _linecastDirection;
        public CollisionsObserver Observer => _observer;

        public VisualSphere Sphere;

        public void FetchDefaultMaterialAndMeshRenderer()
        {
            if (TryGetComponent(out MeshRenderer meshRenderer))
            {
                _meshRenderer = meshRenderer;
            }

            else
            {
                Debug.LogError("No MeshRenderer found");
            }
            
            _defaultMaterial = _meshRenderer.material;
        }

        public void SetMaterial(bool isDefault)
        {
            _meshRenderer.material = isDefault ? _defaultMaterial : _prohibitingMaterial;
        }

        public void SetMaterialToDefault()
        {
            _meshRenderer.material = _defaultMaterial;
        }

        #endregion
    }
}