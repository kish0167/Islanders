using UnityEngine;
using UnityEngine.Serialization;

namespace Islanders
{
    public class BuildingsPlacer : MonoBehaviour
    {
        #region Variables
        
        [Header("Debug only")]
        [SerializeField] private GameObject _startBuildingPrefab;

        [Header("Options")]
        [SerializeField] private float _maxDistance = 100f;

        private GameObject _building;

        private Vector3? _cursorPosition;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            SetBuilding(_startBuildingPrefab);
        }

        private void Update()
        {
            CastARay();
            MoveBuildingWithCursor();
            if (Input.GetMouseButtonDown(0))
            {
                Place();
                SetBuilding(_startBuildingPrefab);
            }
        }

        #endregion

        #region Public methods

        public void SetBuilding(GameObject buildingPrefab)
        {
            if (_building != null)
            {
                Destroy(_building);
            }
            
            _building = Instantiate(buildingPrefab, _cursorPosition ?? Vector3.zero, Quaternion.identity);

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
            if (_building == null)
            {
                return;
            }
            
            if (_cursorPosition == null)
            {
                _building.SetActive(false);
            }
            else
            {
                _building.SetActive(true);
                _building.transform.position = _cursorPosition ?? Vector3.zero;
            }
        }

        private void Place()
        {
            if (_building.TryGetComponent(out Collider cl))
            {
                cl.enabled = true;
            }

            _building = null;
        }

        #endregion
    }
}