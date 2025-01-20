using Islanders.Game.Buildings_placing;
using Islanders.Game.UI;
using Zenject;

namespace Islanders.Game.GameStates
{
    public class MenuState : GameState
    {
        private MenuScreen _menuScreen;
        
        [Inject]
        public MenuState(MenuScreen menuScreen)
        {
            _menuScreen = menuScreen;
        }
        public override void Enter()
        {
            _menuScreen.Show();
        }

        public override void Exit()
        {
            _menuScreen.Hide();
            
        }
    }
}