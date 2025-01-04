using System;
using Lean.Pool;
using UnityEditor.Hardware;
using UnityEngine;

namespace Islanders.Game.Buildings_placing
{
    public class BuildingsPlacer : MonoBehaviour
    {
        #region Variables

        [Header("Debug only")]
        [SerializeField] private PlaceableObject _buildingPrefab;

        [Header("Options")]
        [SerializeField] private float _maxDistance = 100f;
        [SerializeField] private Material _prohibitingMaterial;

        private PlaceableObject _building;

        [Header("Required components")]
        [SerializeField] private PlacingChecker _checker;
        
        private Vector3? _cursorPosition;
        private Material _defaultMaterial;
        private bool _defaultMaterialIsSet;
        private bool _placingPossible;

        #endregion

        #region Events

        public event Action<GameObject, Vector3> OnBuldingPlaced;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            SetBuilding(_buildingPrefab);
        }

        private void Update()
        {
            CastARay();
            MoveBuildingWithCursor();
            if (Input.GetMouseButtonDown(0) && _placingPossible) // input system
            {
                Place();
                SetBuilding(_buildingPrefab);
            }
        }

        #endregion

        #region Public methods

        public void SetBuilding(PlaceableObject buildingPrefab)
        {
            _buildingPrefab = buildingPrefab;

            if (_building != null)
            {
                Destroy(_building);
            }

            _building = LeanPool.Spawn(buildingPrefab, _cursorPosition ?? Vector3.zero, Quaternion.identity);

            UpdateDefaultMaterial();
            _checker.SetBuilding(_building);
        }

        #endregion

        #region Private methods

        private void CastARay()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance))
            {
                _cursorPosition = hit.point;
            }
            else
            {
                _cursorPosition = null;
            }
        }

        private void CheckPlacingPossibility()
        {
            if (_building == null)
            {
                _placingPossible = false;
                return;
            }

            _placingPossible = _checker.IsPossible();
        }

        private void MoveBuildingWithCursor() // TODO: maybe use DoTween
        {
            if (_building == null && _cursorPosition == null)
            {
                return;
            }

            if (_cursorPosition == null)
            {
                LeanPool.Despawn(_building);
                _building = null;
                return;
            }

            if (_building == null)
            {
                _building = LeanPool.Spawn(_buildingPrefab, _cursorPosition ?? Vector3.zero, Quaternion.identity);
            }
            else
            {
                _building.transform.position = _cursorPosition ?? Vector3.zero;
            }

            CheckPlacingPossibility();
            UpdateBuildingMaterial();
        }

        private void Place()
        {
            _building = null;
        }

        private void UpdateBuildingMaterial()
        {
            if (!_building.TryGetComponent(out MeshRenderer meshRenderer))
            {
                return;
            }

            if (_placingPossible && !_defaultMaterialIsSet)
            {
                meshRenderer.material = _defaultMaterial;
                _defaultMaterialIsSet = true;
            }
            else if (!_placingPossible && _defaultMaterialIsSet)
            {
                meshRenderer.material = _prohibitingMaterial;
                _defaultMaterialIsSet = false;
            }
        }

        private void UpdateDefaultMaterial()
        {
            if (_building.TryGetComponent(out MeshRenderer meshRenderer))
            {
                _defaultMaterial = meshRenderer.material;
            }
        }

        #endregion
    }
}