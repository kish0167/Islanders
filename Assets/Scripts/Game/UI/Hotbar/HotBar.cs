using System.Collections.Generic;
using System.Linq;
using Islanders.Game.Buildings_placing;
using Islanders.Utils.Log;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Islanders.Game.UI.Hotbar
{
    public class HotBar : MonoBehaviour
    {
        #region Variables

        [Header("Buttons")]
        [SerializeField] private List<HotBarButton> _buildingsButtons;
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;
        [SerializeField] private Button _newBuildingButton;

        private Dictionary<PlaceableObject, int> _allBuildings;
        private HotBarButtonFactory _buttonFactory;
        private int _overflowPointer;
        private Player.Player _player;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(Player.Player player, HotBarButtonFactory factory)
        {
            _player = player;

            _buttonFactory = factory;

            foreach (HotBarButton button in _buildingsButtons)
            {
                _buttonFactory.Setup(button);
            }

            _leftArrow.onClick.AddListener(LeftArrowPressedCallback);
            _rightArrow.onClick.AddListener(RightArrowPressedCallback);
            _newBuildingButton.onClick.AddListener(NewBuildingButtonPressedCallback);
        }

        #endregion

        #region Unity lifecycle

        private void OnEnable()
        {
            _player.OnInventoryUpdated += InventoryUpdatedCallback;
        }

        private void OnDisable()
        {
            _player.OnInventoryUpdated -= InventoryUpdatedCallback;
        }

        #endregion

        #region Private methods

        private void InventoryUpdatedCallback(Dictionary<PlaceableObject, int> inventory, PlaceableObject selected)
        {
            _allBuildings = inventory;

            if (inventory.Count <= _buildingsButtons.Count)
            {
                UpdateNotFullBar();
            }
            else
            {
                UpdateOverflowedBar();
            }
        }

        private void LeftArrowPressedCallback()
        {
            throw new System.NotImplementedException();
        }

        private void NewBuildingButtonPressedCallback()
        {
            throw new System.NotImplementedException();
        }

        private void RightArrowPressedCallback()
        {
            throw new System.NotImplementedException();
        }

        private void UpdateNotFullBar()
        {
            _leftArrow.gameObject.SetActive(false);
            _rightArrow.gameObject.SetActive(false);

            for (int i = 0; i < _allBuildings.Count; i++)
            {
                ActivateButton(i, i);
            }

            for (int i = _allBuildings.Count; i < _buildingsButtons.Count; i++)
            {
                _buildingsButtons[i].gameObject.SetActive(false);
            }

            _overflowPointer = 0;
        }

        private void UpdateOverflowedBar()
        {
            if (_overflowPointer > _allBuildings.Count - _buildingsButtons.Count)
            {
                this.Error("index out of buttons range");
                return;
            }

            _leftArrow.gameObject.SetActive(true);
            _rightArrow.gameObject.SetActive(true);
            
            for (int i = 0; i < _buildingsButtons.Count; i++)
            {
                ActivateButton(i, i + _overflowPointer);
            }
        }

        private void ActivateButton(int buttonIndex, int prefabIndex)
        {
            PlaceableObject[] buildings = _allBuildings.Keys.ToArray();
            HotBarButton btn = _buildingsButtons[buttonIndex];
            btn.gameObject.SetActive(true);
            btn.Prefab = buildings[prefabIndex];
            btn.Quantity = _allBuildings[buildings[prefabIndex]];
            btn.UpdateLabels();
        }

        #endregion
    }
}