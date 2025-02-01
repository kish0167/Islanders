using Islanders.Game.LocalInput;
using Islanders.Game.Pause;
using Islanders.Game.UI.Hotbar;
using Islanders.Game.Utility;
using Zenject;

namespace Islanders.Game.UI
{
    public class UiInput : IInputControllable
    {
        #region Variables

        private HotBar _hotBar;
        private readonly PauseService _pause;

        #endregion

        #region Setup/Teardown

        [Inject]
        public UiInput(PauseService pause, HotBar hotBar)
        {
            _pause = pause;
            _hotBar = hotBar;
        }

        #endregion

        #region IInputControllable

        public void DoAction(KeyBind bind)
        {
            if (bind == KeyBind.Pause)
            {
                _pause.Toggle();
            }
            else if (bind >= KeyBind.HotBar1 && bind < KeyBind.UiEnd)
            {
                _hotBar.DoAction(bind);
            }
        }

        #endregion
    }
}