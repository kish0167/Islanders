using Lean.Pool;
using UnityEngine;

namespace Islanders.Game.Buildings_placing
{
    public class BuildingsPlacer : MonoBehaviour
    {
        #region Variables

        [Header("Debug only")]
        [SerializeField] private GameObject _buildingPrefab;

        [Header("Options")]
        [SerializeField] private float _maxDistance = 100f;

        private GameObject _building;

        private Vector3? _cursorPosition;

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
            if (Input.GetMouseButtonDown(0))
            {
                Place();
                SetBuilding(_buildingPrefab);
            }
        }

        #endregion

        #region Public methods

        public void SetBuilding(GameObject buildingPrefab)
        {
            _buildingPrefab = buildingPrefab;

            if (_building != null)
            {
                Destroy(_building);
            }

            _building = LeanPool.Spawn(buildingPrefab, _cursorPosition ?? Vector3.zero, Quaternion.identity);

            if (_building.TryGetComponent(out Collider cl))
            {
                cl.enabled = false;
            }
        }

        #endregion

        #region Private methods

        private void CastARay()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // TODO: maybe check Camera.main
            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance))
            {
                _cursorPosition = hit.point;
            }
            else
            {
                _cursorPosition = null;
            }
        }

        private void MoveBuildingWithCursor() // TODO: use DoTween
        {
            if (_cursorPosition == null && _building != null)
            {
                LeanPool.Despawn(_building);
                _building = null;
                return;
            }

            if (_cursorPosition != null && _building == null)
            {
                _building = LeanPool.Spawn(_buildingPrefab, _cursorPosition ?? Vector3.zero, Quaternion.identity);
                return;
            }

            if (_building != null)
            {
                _building.transform.position = _cursorPosition ?? Vector3.zero;
            }
        }

        private void Place()
        {
            if (_building.TryGetComponent(out Collider cl))
            {
                //cl.enabled = true;
            }

            _building = null;
        }

        #endregion
    }
}