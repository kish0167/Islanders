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

        private Player.Player _player;
        private static Color _defaultColor = new Color(0,0,0,0.7f);

        #endregion

        #region Properties

        public PlaceableObject Prefab { get; set; }
        public int Quantity { get; set; }

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _button.onClick.AddListener(PressedCallback);
        }

        #endregion

        #region Public methods

        public void Construct(Player.Player player)
        {
            _player = player;
        }

        public void UpdateLabels()
        {
            _nameLabel.text = Prefab.name;
            _quantityLabel.text = Quantity.ToString();
        }

        #endregion

        #region Private methods

        private void PressedCallback()
        {
            _player.SelectBuilding(Prefab);
        }

        #endregion

        public void Highlight()
        {
            _button.image.color = new Color(0, 0, 0, 0.9f);
        }

        public void RemoveHighlighting()
        {
            _button.image.color = _defaultColor;
        }
    }
}