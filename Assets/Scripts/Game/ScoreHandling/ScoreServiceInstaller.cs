using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Islanders.Game.ScoreHandling
{
    public class ScoreServiceInstaller : Installer<ScoreServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ScoreTableService>().FromNew().AsSingle().NonLazy();
        }
    }
}