using TMPro;
using UnityEngine;

namespace Islanders.Game.UI
{
    public class ScoreLabel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreFloorLabel;
        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private TMP_Text _scoreCeilingLabel;
        
        
        public void UpdateLabels(int scoreFloor, int score, int scoreCeiling)
        {
            _scoreFloorLabel.text = scoreFloor.ToString();
            _scoreLabel.text = score.ToString();
            _scoreCeilingLabel.text = scoreCeiling.ToString();
        }
    }
}