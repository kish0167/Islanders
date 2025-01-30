using UnityEngine;

namespace Islanders.Game.UI
{
    public class ChoiceScreen : MonoBehaviour, IScreen
    {
        [SerializeField] private GameObject _content;
        
        #region IScreen

        public void Hide()
        {
            _content.SetActive(false);
        }

        public void Show()
        {
            _content.SetActive(true);
        }
        

        #endregion
    }
}