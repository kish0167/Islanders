using UnityEngine.SceneManagement;

namespace Islanders.Game.GameStates
{
    public class GoToNewIslandState : GameState
    {
        public override void Enter()
        {
            SceneManager.LoadScene("SampleScene");
        }

        public override void Exit()
        {
            
        }
    }
}