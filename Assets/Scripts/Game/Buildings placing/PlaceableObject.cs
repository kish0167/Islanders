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

        [HideInInspector]
        public VisualSphere Sphere;

        [Header("Options")]
        [SerializeField] private List<string> _allowedTags;
        [SerializeField] private Vector3 _linecastDirection = new Vector3(0,-1,0);

        [Header("Required components")]
        [SerializeField] private CollisionsObserver _observer;
        private Material _defaultMaterial;

        private MeshRenderer _meshRenderer;
        private readonly List<MeshRenderer> _meshRenderers = new();
        private Material _prohibitingMaterial;

        #endregion

        #region Properties

        public List<string> AllowedTags => _allowedTags;
        public Vector3 LinecastDirection => _linecastDirection;
        public CollisionsObserver Observer => _observer;

        public Material ProhibitingMaterial
        {
            get => _prohibitingMaterial;
            set => _prohibitingMaterial = value;
        }

        #endregion

        #region Public methods

        public void FetchDefaultMaterialAndMeshRenderer()
        {
            _meshRenderers.Clear();
            AddChildrenMeshRenderersRecursive(gameObject);
            return;

            void AddChildrenMeshRenderersRecursive(GameObject obj)
            {
                if (obj.TryGetComponent(out MeshRenderer meshRenderer))
                {
                    _meshRenderers.Add(meshRenderer);
                    _defaultMaterial = meshRenderer.material;
                }

                for (int i = 0; i < obj.transform.childCount; i++)
                {
                    AddChildrenMeshRenderersRecursive(obj.transform.GetChild(i).gameObject);
                }
            }
        }

        public void SetMaterial(bool isDefault)
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers)
            {
                meshRenderer.material = isDefault ? _defaultMaterial : _prohibitingMaterial;
            }

            //_meshRenderer.material = isDefault ? _defaultMaterial : _prohibitingMaterial;
        }

        public void SetMaterialToDefault()
        {
            foreach (MeshRenderer meshRenderer in _meshRenderers)
            {
                meshRenderer.material = _defaultMaterial;
            }

            //_meshRenderer.material = _defaultMaterial;
        }

        #endregion
    }
}