using Islanders.Game.Buildings_placing;
using Islanders.Game.GameScript;
using Islanders.Game.GameStates;
using Islanders.Game.LocalCamera;
using Islanders.Game.LocalInput;
using Islanders.Game.Pause;
using Islanders.Game.Player;
using Islanders.Game.UI;
using Islanders.Game.UI.Hotbar;
using Islanders.Game.UI.HoveringLabels;
using Islanders.Game.Utility;
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
        [FormerlySerializedAs("_playerHotBar")] [SerializeField] private PlacingScreen _placingScreen;
        [SerializeField] private MenuScreen _menuScreen;
        [SerializeField] private ChoiceScreen _choiceScreen;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private HotBar _hotBar;
        [FormerlySerializedAs("_scoreLabel")] [SerializeField] private ScoreBox _scoreBox;
        

        #endregion

        #region Public methods

        public override void InstallBindings()
        {
            Container.Bind<PlaceableObjectFactory>().FromNew().AsSingle();
            Container.Bind<BuildingsPlacer>().FromInstance(_buildingsPlacer).AsSingle();
            Container.Bind<PrefabsProvider>().FromInstance(_prefabProvider).AsSingle();
            Container.Bind<PlayerInventory>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<LocalStateMachine>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<PlayerScore>().FromNew().AsSingle().NonLazy();
            Container.Bind<LocalInputService>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<KeysActions>().FromNew().AsSingle().NonLazy();
            Container.Bind<PauseService>().FromNew().AsSingle();
            Container.Bind<UiInput>().FromNew().AsSingle().NonLazy();
            Container.Bind<CameraMovement>().FromInstance(_cameraMovement).AsSingle();
            Container.Bind<HotBar>().FromInstance(_hotBar).AsSingle();
            Container.Bind<HotBarButtonFactory>().FromNew().AsSingle();
            Container.Bind<ScoreBox>().FromInstance(_scoreBox).AsSingle();
            Container.Bind<IScriptService>().To<LinearScriptService>().FromNew().AsSingle().NonLazy();
            Container.Bind<HoveringLabelsService>().FromNew().AsSingle().NonLazy();
            Container.Bind<HoveringLabelsFactory>().FromNew().AsSingle();


            // screens
            Container.Bind<MenuScreen>().FromInstance(_menuScreen).AsSingle();
            Container.Bind<ChoiceScreen>().FromInstance(_choiceScreen).AsSingle().NonLazy();
            Container.Bind<PlacingScreen>().FromInstance(_placingScreen).AsSingle().NonLazy();

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