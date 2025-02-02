using System;
using Islanders.Game.Utility;
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

        [Header("Required components")]
        [SerializeField] private PlacingChecker _checker;
        private PlaceableObject _building;
        private Vector3? _cursorPosition;
        private bool _isPlacing;

        private Camera _mainCamera;
        private PlaceableObjectFactory _placeableObjectFactory;
        private bool _placingPossible;

        #endregion

        #region Events 

        public event Action<PlaceableObject, PlaceableObject, Vector3> OnBuildingPlaced;

        #endregion

        #region Properties

        public bool Enabled { get; set; }

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(PlaceableObjectFactory placeableObjectFactory)
        {
            _placeableObjectFactory = placeableObjectFactory;
            _mainCamera = Camera.main;
        }

        #endregion

        #region Unity lifecycle

        private void Update()
        {
            if (!Enabled || !_isPlacing)
            {
                return;
            }

            CastARay();
            MoveBuildingWithCursor();
            CheckPlacingPossibility();
            UpdateBuildingMaterial();

            if (Input.GetMouseButtonDown(0) && _placingPossible) // input system
            {
                Place();
            }
        }

        #endregion

        #region Public methods

        public void Disable()
        {
            Enabled = false;
        }

        public void Enable()
        {
            Enabled = true;
        }

        public void SetBuilding(PlaceableObject buildingPrefab)
        {
            _isPlacing = true;
            _buildingPrefab = buildingPrefab;

            if (_building != null)
            {
                _placeableObjectFactory.Deconstruct(_building);
            }

            SpawnBuildingFromPrefab();

            _checker.SetBuilding(_building);

            Enable();
        }

        #endregion

        #region Private methods

        private void CastARay()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Input.mousePosition.y <= Screen.height * 0.12)
            {
                _cursorPosition = null;
                return;
            }

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
                _placeableObjectFactory.Deconstruct(_building);

                _building = null;
                return;
            }

            if (_building == null)
            {
                SpawnBuildingFromPrefab();
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
            _building.Sphere.Dispose();
            PlaceableObject buf = _building;
            _building = null;
            Enabled = false;
            _isPlacing = false;
            Disable();

            OnBuildingPlaced?.Invoke(buf, _buildingPrefab, _cursorPosition ?? Vector3.zero);
            _buildingPrefab = null;
        }

        private void SpawnBuildingFromPrefab()
        {
            _building = _placeableObjectFactory.CreateFromPrefab(_buildingPrefab, _cursorPosition ?? Vector3.zero);
        }

        private void UpdateBuildingMaterial()
        {
            if (_building == null)
            {
                return;
            }

            _building.SetMaterial(_placingPossible);
        }

        #endregion
    }
}