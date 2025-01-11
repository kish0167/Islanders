using UnityEngine.Rendering;
using Zenject;

namespace Islanders.Game.ScoreHandling
{
    public class ScoreServiceInstaller : Installer<ScoreServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ScoreService>().FromNew().AsSingle().NonLazy();
        }
    }
}