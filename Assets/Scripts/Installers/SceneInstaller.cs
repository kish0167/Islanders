using Islanders.Game.Buildings_placing;
using Islanders.Game.ScoreHandling;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Islanders.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private BuildingsPlacer _buildingsPlacer;
        
        public override void InstallBindings()
        {
            Container.Bind<PlaceableObjectFactory>().FromNew().AsSingle();
            Container.Bind<BuildingsPlacer>().FromInstance(_buildingsPlacer).AsSingle().NonLazy();
            Debug.Log("scene context installed");
        }
    }
}