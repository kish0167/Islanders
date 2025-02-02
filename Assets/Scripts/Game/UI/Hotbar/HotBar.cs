using System;
using System.Collections.Generic;
using System.Linq;
using Islanders.Game.Buildings_placing;
using Islanders.Game.GameStates;
using Islanders.Game.LocalInput;
using Islanders.Game.Undo;
using Islanders.Game.Utility;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Islanders.Game.UI.Hotbar
{
    public class HotBar : MonoBehaviour, IInputControllable
    {
        #region Variables

        [Header("Buttons")]
        [SerializeField] private List<HotBarButton> _buildingsButtons;
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;
        [SerializeField] private Button _newBuildingButton;
        [SerializeField] private Button _undoButton;

        private Dictionary<PlaceableObject, int> _allBuildings;
        private HotBarButtonFactory _buttonFactory;
        private int _overflowPointer;
        private Player.PlayerInventory _playerInventory;
        private LocalStateMachine _stateMachine;
        private UndoService _undoService;

        #endregion

        #region Events

        public event Action OnUndoButtonPressed;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(Player.PlayerInventory playerInventory, HotBarButtonFactory factory,
            LocalStateMachine stateMachine, UndoService undoService)
        {
            _playerInventory = playerInventory;
            _buttonFactory = factory;
            _stateMachine = stateMachine;
            _undoService = undoService;

            foreach (HotBarButton button in _buildingsButtons)
            {
                _buttonFactory.Setup(button);
            }

            _leftArrow.onClick.AddListener(LeftArrowPressedCallback);
            _rightArrow.onClick.AddListener(RightArrowPressedCallback);
            _newBuildingButton.onClick.AddListener(NewBuildingButtonPressedCallback);
            _undoButton.onClick.AddListener(UndoButtonPressedCallback);
        }

        #endregion

        #region Unity lifecycle

        private void OnEnable()
        {
            _playerInventory.OnInventoryUpdated += InventoryUpdatedCallback;
            _playerInventory.ForceUiUpdate();
        }

        private void OnDisable()
        {
            _playerInventory.OnInventoryUpdated -= InventoryUpdatedCallback;
        }

        #endregion

        #region IInputControllable

        public void DoAction(KeyBind key)
        {
            if (!_buildingsButtons[key - KeyBind.HotBar1].gameObject.activeSelf)
            {
                return;
            }

            _playerInventory.SelectBuilding(_buildingsButtons[key - KeyBind.HotBar1].Prefab);
        }

        #endregion

        #region Public methods

        public void HideNewBuildingsButton()
        {
            _newBuildingButton.gameObject.SetActive(false);
        }

        public void ShowNewBuildingsButton()
        {
            _newBuildingButton.gameObject.SetActive(true);
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

            if (inventory.Count <= _buildingsButtons.Count)
            {
                UpdateNotFullBar();
            }
            else
            {
                UpdateOverflowedBar();
            }

            _undoButton.gameObject.SetActive(_undoService.Available);

            HighLightButtonWith(selected);
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

        private void UndoButtonPressedCallback()
        {
            OnUndoButtonPressed?.Invoke();
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
    }
}