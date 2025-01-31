namespace Islanders.Game.GameScript
{
    public interface IScriptService
    {
        public void ProceedToNextStep(){}
        public void ChoiceMadeCallback(int choice);
    }
}