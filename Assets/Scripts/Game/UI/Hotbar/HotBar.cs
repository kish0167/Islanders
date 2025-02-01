using System;
using System.Collections.Generic;
using System.Linq;
using Islanders.Game.Buildings_placing;
using Islanders.Game.GameStates;
using Islanders.Game.LocalInput;
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
        private LocalInputService _inputService;
        private int _overflowPointer;
        private Player.PlayerInventory _playerInventory;
        private PlaceableObject _selectedBuilding;
        private LocalStateMachine _stateMachine;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(Player.PlayerInventory playerInventory, HotBarButtonFactory factory,
            LocalStateMachine stateMachine, LocalInputService inputService)
        {
            _playerInventory = playerInventory;
            _buttonFactory = factory;
            _stateMachine = stateMachine;
            _inputService = inputService;

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
            _playerInventory.OnInventoryUpdated += InventoryUpdatedCallback;
            _inputService.OnKeyPressed += KeyPressedCallback;
            _playerInventory.ForceUiUpdate();
        }

        private void OnDisable()
        {
            _playerInventory.OnInventoryUpdated -= InventoryUpdatedCallback;
            _inputService.OnKeyPressed -= KeyPressedCallback;
        }

        #endregion

        #region Private methods

        private void ActivateButton(int buttonIndex, int prefabIndex)
        {
            PlaceableObject[] buildings = _allBuildings.Keys.ToArray();
            HotBarButton btn = _buildingsButtons[buttonIndex];
            btn.gameObject.SetActive(true);
            btn.Prefab = buildings[prefabIndex];
            btn.Quantity = _allBuildings[buildings[prefabIndex]];
            btn.UpdateLabels();
        }

        private void AdjustOverflowPointer()
        {
            _overflowPointer = Math.Max(_overflowPointer, 0);
            _overflowPointer = Math.Min(_overflowPointer, _allBuildings.Count - _buildingsButtons.Count);
        }

        private void HighLightButtonWith(PlaceableObject selected)
        {
            foreach (HotBarButton button in _buildingsButtons)
            {
                if (!button.enabled)
                {
                    continue;
                }
                if (button.Prefab == selected)
                {
                    button.Highlight();
                }
                else
                {
                    button.RemoveHighlighting();
                }
            }
        }

        private void InventoryUpdatedCallback(Dictionary<PlaceableObject, int> inventory, PlaceableObject selected)
        {
            _allBuildings = inventory;
            _selectedBuilding = selected;
            
            if (inventory.Count <= _buildingsButtons.Count)
            {
                UpdateNotFullBar();
            }
            else
            {
                UpdateOverflowedBar();
            }
            
            HighLightButtonWith(selected);
        }

        private void KeyPressedCallback(KeyBind key)
        {
            if (key < KeyBind.HotBar1 || key >= KeyBind.UiEnd || !_buildingsButtons[key - KeyBind.HotBar1].enabled)
            {
                return;
            }

            _playerInventory.SelectBuilding(_buildingsButtons[key - KeyBind.HotBar1].Prefab);
        }

        private void LeftArrowPressedCallback()
        {
            _overflowPointer = Math.Max(_overflowPointer - 1, 0);
            _playerInventory.ForceUiUpdate();
        }

        private void NewBuildingButtonPressedCallback()
        {
            _stateMachine.TransitionTo<ChoosingState>();
        }

        private void RightArrowPressedCallback()
        {
            _overflowPointer = Math.Min(_overflowPointer + 1, _allBuildings.Count - _buildingsButtons.Count);
            _playerInventory.ForceUiUpdate();
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
            AdjustOverflowPointer();

            _leftArrow.gameObject.SetActive(true);
            _rightArrow.gameObject.SetActive(true);

            for (int i = 0; i < _buildingsButtons.Count; i++)
            {
                ActivateButton(i, i + _overflowPointer);
            }
        }

        #endregion

        public void ShowNewBuildingsButton()
        {
            _newBuildingButton.gameObject.SetActive(true);
        }

        public void HideNewBuildingsButton()
        {
            _newBuildingButton.gameObject.SetActive(false);
        }
    }
}