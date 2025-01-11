using System;
using Islanders.Game.Utility;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Islanders.Game.ScoreHandling
{
    public class ScoreCounter : MonoBehaviour
    {
        #region Variables

        [SerializeField] private ObjectType _objectType;
        [SerializeField] private float _radius;

        private ScoreService _scoreService;
        private bool _isPlaced = false;

        #endregion

        #region Properties

        public ObjectType Type => _objectType;

        public void OnDrawGizmos()
        {
            if (_isPlaced)
            {
                return;
            }
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, _radius);
        }

        [Inject]
        private void Construct(ScoreService service)
        {
            Debug.Log("sfasfa");
            _scoreService = service;
        }

        private void Update()
        {
            Collider[] hits = Array.Empty<Collider>();
            Physics.OverlapSphereNonAlloc(gameObject.transform.position, _radius, hits);

            //Debug.LogError(hits.Length);
            
            foreach (Collider hit in hits)
            {
                Debug.Log(hit.gameObject.name);
            }
        }

        #endregion
    }
}