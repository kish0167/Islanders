using Islanders.Game.Buildings_placing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Islanders.Game.UI.Hotbar
{
    public class HotBarButton : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _quantityLabel;
        [SerializeField] private TMP_Text _nameLabel;

        private BuildingsPlacer _placer;

        #endregion

        #region Properties

        public PlaceableObject Prefab { get; set; }
        public string PrefabName { get; set; }
        public int Quantity { get; set; }

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _button.onClick.AddListener(PressedCallback);
        }

        #endregion

        #region Public methods

        public void Construct(BuildingsPlacer placer)
        {
            _placer = placer;
        }

        #endregion

        #region Private methods

        private void PressedCallback()
        {
            if (Prefab == null || Quantity < 1)
            {
                return;
            }

            _placer.SetBuilding(Prefab);
        }

        #endregion
    }
}