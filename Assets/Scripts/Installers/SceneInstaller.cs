using Islanders.Game.Buildings_placing;
using Islanders.Game.Player;
using Islanders.Game.ScoreHandling;
using Islanders.Game.Utility;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Islanders.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private BuildingsPlacer _buildingsPlacer;
        [SerializeField] private PrefabsProvider _prefabProvider;
        
        public override void InstallBindings()
        {
            Container.Bind<PlaceableObjectFactory>().FromNew().AsSingle();
            Container.Bind<BuildingsPlacer>().FromInstance(_buildingsPlacer).AsSingle();
            Container.Bind<PrefabsProvider>().FromInstance(_prefabProvider).AsSingle();
            Container.Bind<Player>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Debug.Log("scene context installed");
        }
    }
}