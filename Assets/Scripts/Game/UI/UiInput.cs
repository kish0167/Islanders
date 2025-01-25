using Islanders.Game.LocalInput;
using Islanders.Game.Pause;
using Islanders.Game.Utility;
using Zenject;

namespace Islanders.Game.UI
{
    public class UiInput : IInputControllable
    {
        private PauseService _pause;
        
        [Inject]
        public UiInput(PauseService pause)
        {
            _pause = pause;
        }

        public void DoAction(KeyBind bind)
        {
            if (bind == KeyBind.Pause)
            {
                _pause.Toggle();
            }
        }
    }
}