using Islanders.Game.ScoreHandling;
using UnityEngine;
using Zenject;

namespace Islanders.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            
            Debug.Log("scene context installed");
        }
    }
}