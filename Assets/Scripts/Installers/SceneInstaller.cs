using Islanders.Game.Buildings_placing;
using Islanders.Game.GameStates;
using Islanders.Game.LocalInput;
using Islanders.Game.Pause;
using Islanders.Game.Player;
using Islanders.Game.UI;
using Islanders.Game.Utility;
using UnityEngine;
using Zenject;

namespace Islanders.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        #region Variables

        [SerializeField] private BuildingsPlacer _buildingsPlacer;
        [SerializeField] private PrefabsProvider _prefabProvider;
        [SerializeField] private PlayerHotBar _playerHotBar;
        [SerializeField] private MenuScreen _menuScreen;
        [SerializeField] private ChoiceScreen _choiceScreen;

        #endregion

        #region Public methods

        public override void InstallBindings()
        {
            Container.Bind<PlaceableObjectFactory>().FromNew().AsSingle();
            Container.Bind<BuildingsPlacer>().FromInstance(_buildingsPlacer).AsSingle();
            Container.Bind<PrefabsProvider>().FromInstance(_prefabProvider).AsSingle();
            Container.Bind<Player>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<PlayerHotBar>().FromInstance(_playerHotBar).AsSingle().NonLazy();
            Container.Bind<LocalStateMachine>().FromNew().AsSingle().NonLazy();
            Container.Bind<PlayerScore>().FromNew().AsSingle().NonLazy();
            Container.Bind<LocalInputService>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<KeysActions>().FromNew().AsSingle().NonLazy();
            Container.Bind<PauseService>().FromNew().AsSingle().NonLazy();

            // screens
            Container.Bind<MenuScreen>().FromInstance(_menuScreen).AsSingle().NonLazy();
            Container.Bind<ChoiceScreen>().FromInstance(_choiceScreen).AsSingle().NonLazy();

            // states
            Container.Bind<BootsTrapState>().FromNew().AsSingle().NonLazy();
            Container.Bind<PlacingState>().FromNew().AsSingle().NonLazy();
            Container.Bind<MenuState>().FromNew().AsSingle().NonLazy();
            Container.Bind<ChoosingState>().FromNew().AsSingle().NonLazy();
            Container.Bind<GoToNewIslandState>().FromNew().AsSingle().NonLazy();

            Debug.Log("scene context installed");
        }

        #endregion
    }
}