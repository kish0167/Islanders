using Islanders.Game.Buildings_placing;
using Islanders.Game.GameOver;
using Islanders.Game.GameScript;
using Islanders.Game.GameStates;
using Islanders.Game.LocalCamera;
using Islanders.Game.LocalInput;
using Islanders.Game.Pause;
using Islanders.Game.Player;
using Islanders.Game.UI;
using Islanders.Game.UI.Hotbar;
using Islanders.Game.UI.HoveringLabels;
using Islanders.Game.UI.ScoreBox;
using Islanders.Game.Undo;
using Islanders.Game.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Islanders.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        #region Variables

        [SerializeField] private BuildingsPlacer _buildingsPlacer;
        [SerializeField] private PrefabsProvider _prefabProvider;
        [SerializeField] private PlacingScreen _placingScreen;
        [SerializeField] private MenuScreen _menuScreen;
        [SerializeField] private ChoiceScreen _choiceScreen;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private HotBar _hotBar;
        [SerializeField] private ScoreBox _scoreBox;

        #endregion

        #region Public methods

        public override void InstallBindings()
        {
            // Local state machine
            Container.Bind<LocalStateMachine>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            
            // Factories
            Container.Bind<PlaceableObjectFactory>().FromNew().AsSingle();
            Container.Bind<HotBarButtonFactory>().FromNew().AsSingle();
            Container.Bind<HoveringLabelsFactory>().FromNew().AsSingle();

            // Services
            Container.Bind<PauseService>().FromNew().AsSingle();
            Container.Bind<UiInput>().FromNew().AsSingle().NonLazy();
            Container.Bind<LocalInputService>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<HoveringLabelsService>().FromNew().AsSingle().NonLazy();
            Container.Bind<GameOverService>().FromNew().AsSingle().NonLazy();
            Container.Bind<UndoService>().FromNew().AsSingle().NonLazy();
            Container.Bind<IScriptService>().To<LinearScriptService>().FromNew().AsSingle().NonLazy();
            
            // Player-related bindings
            Container.Bind<PlayerInventory>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<PlayerScore>().FromNew().AsSingle().NonLazy();
            Container.Bind<KeysActions>().FromNew().AsSingle().NonLazy();

            // Game logic
            Container.Bind<BuildingsPlacer>().FromInstance(_buildingsPlacer).AsSingle();
            Container.Bind<PrefabsProvider>().FromInstance(_prefabProvider).AsSingle();
            Container.Bind<CameraMovement>().FromInstance(_cameraMovement).AsSingle();
            Container.Bind<HotBar>().FromInstance(_hotBar).AsSingle();
            Container.Bind<ScoreBox>().FromInstance(_scoreBox).AsSingle();

            // Screens
            Container.Bind<MenuScreen>().FromInstance(_menuScreen).AsSingle();
            Container.Bind<ChoiceScreen>().FromInstance(_choiceScreen).AsSingle().NonLazy();
            Container.Bind<PlacingScreen>().FromInstance(_placingScreen).AsSingle().NonLazy();
            Container.Bind<GameOverScreen>().FromInstance(_gameOverScreen).AsSingle().NonLazy();

            // States
            Container.Bind<BootsTrapState>().FromNew().AsSingle().NonLazy();
            Container.Bind<MenuState>().FromNew().AsSingle().NonLazy();
            Container.Bind<ChoosingState>().FromNew().AsSingle().NonLazy();
            Container.Bind<PlacingState>().FromNew().AsSingle().NonLazy();
            Container.Bind<GoToNewIslandState>().FromNew().AsSingle().NonLazy();
            Container.Bind<GameOverState>().FromNew().AsSingle().NonLazy();

            Debug.Log("scene context installed");
        }

        #endregion
    }
}