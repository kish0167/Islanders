using System;
using Islanders.Game.Utility;
using Lean.Pool;
using UnityEngine;
using Zenject;

namespace Islanders.Game.Buildings_placing
{
    [RequireComponent(typeof(PlacingChecker))]
    public class BuildingsPlacer : MonoBehaviour
    {
        #region Variables

        [Header("Debug only")]
        [SerializeField] private PlaceableObject _buildingPrefab;

        [Header("Options")]
        [SerializeField] private float _maxDistance = 100f;
        [SerializeField] private Material _prohibitingMaterial;

        [Header("Required components")]
        [SerializeField] private PlacingChecker _checker;

        private PlaceableObject _building;
        private PlaceableObjectFactory _objectFactory;

        private Vector3? _cursorPosition;
        private Material _defaultMaterial;
        private bool _defaultMaterialIsSet;
        private bool _placingPossible;

        #endregion

        #region Events

        public event Action<PlaceableObject, Vector3> OnBuildingPlaced;

        #endregion

        #region Unity lifecycle

        [Inject]
        public void Construct(PlaceableObjectFactory placeableObjectFactory)
        {
            _objectFactory = placeableObjectFactory;
        }

        private void Start()
        {
            SetBuilding(_buildingPrefab);
        }

        private void Update()
        {
            CastARay();
            MoveBuildingWithCursor();
            CheckPlacingPossibility();
            UpdateBuildingMaterial();

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

            //_building = LeanPool.Spawn(buildingPrefab, _cursorPosition ?? Vector3.zero, Quaternion.identity);
            _building = _objectFactory.CreateFromPrefab(buildingPrefab, _cursorPosition ?? Vector3.zero);
            
            FetchDefaultMaterial();
            _building.gameObject.layer = LayerMask.NameToLayer(Layers.ActiveBuilding);
            _defaultMaterialIsSet = true;
            _checker.SetBuilding(_building);
        }

        #endregion

        #region Private methods

        private void CastARay()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance, ~LayerMask.GetMask(Layers.ActiveBuilding)))
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
                //_building = LeanPool.Spawn(_buildingPrefab, _cursorPosition ?? Vector3.zero, Quaternion.identity);
                _building = _objectFactory.CreateFromPrefab(_buildingPrefab, _cursorPosition ?? Vector3.zero);
                _checker.Reset();
            }
            else
            {
                _building.transform.position = _cursorPosition ?? Vector3.zero;
            }
        }

        private void Place()
        {
            _building.gameObject.layer = LayerMask.NameToLayer(Layers.PlacedBuilding);
            OnBuildingPlaced?.Invoke(_building, _cursorPosition ?? Vector3.zero);
            _building = null;
        }

        private void UpdateBuildingMaterial()
        {
            if (_building == null ||  !_building.TryGetComponent(out MeshRenderer meshRenderer))
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

        private void FetchDefaultMaterial()
        {
            if (_building.TryGetComponent(out MeshRenderer meshRenderer))
            {
                _defaultMaterial = meshRenderer.material;
            }
        }

        #endregion
    }
}