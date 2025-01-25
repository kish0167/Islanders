using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Islanders.Game.GameStates
{
    public class LocalStateMachine : MonoBehaviour
    {
        #region Variables

        private readonly List<GameState> _states = new();

        private GameState _currentState;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(BootsTrapState bootsTrap, MenuState menu, PlacingState placing, ChoosingState choosing,
            GoToNewIslandState newIsland)
        {
            _states.Add(bootsTrap);
            _states.Add(menu);
            _states.Add(placing);
            _states.Add(choosing);
            _states.Add(newIsland);
            _currentState = bootsTrap;
        }

        #endregion

        private void Start()
        {
            _currentState.Enter();
            
            // 
            
            TransitionTo<MenuState>();
        }

        #region Public methods

        public bool Is<T>() where T : GameState
        {
            return typeof(T) == _currentState.GetType();
        }

        public void TransitionTo<T>() where T : GameState
        {
            _currentState.Exit();

            foreach (GameState state in _states)
            {
                if (state.GetType() != typeof(T))
                {
                    continue;
                }

                _currentState = state;
                state.Enter();
                return;
            }

            Debug.LogError($"Sate {nameof(T)} not present in state list");
        }

        #endregion
    }
}