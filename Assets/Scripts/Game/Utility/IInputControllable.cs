using Islanders.Game.LocalInput;

namespace Islanders.Game.Utility
{
    public interface IInputControllable
    {
        public void DoAction(KeyBind key);
    }
}