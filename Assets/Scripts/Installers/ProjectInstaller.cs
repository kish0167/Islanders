using Islanders.Game.GameStates;
using Islanders.Game.ScoreHandling;
using UnityEngine;
using Zenject;

namespace Islanders.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        #region Public methods

        public override void InstallBindings()
        {
            ScoreServiceInstaller.Install(Container);
            
            
            Debug.Log("project context installed");
        }
        #endregion
    }
}