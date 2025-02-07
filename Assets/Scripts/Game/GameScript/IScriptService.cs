namespace Islanders.Game.GameScript
{
    public interface IScriptService
    {
        #region Public methods

        public void ChoiceMadeCallback(int choice);

        public void UpdateUi();

        #endregion
    }
}