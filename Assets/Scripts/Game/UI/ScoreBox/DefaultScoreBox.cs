using TMPro;
using UnityEngine;

namespace Islanders.Game.UI.ScoreBox
{
    public class DefaultScoreBox : ScoreBox
    {
        #region Variables

        [SerializeField] private TMP_Text _scoreFloorLabel;
        [SerializeField] private TMP_Text _scoreLabel;
        [SerializeField] private TMP_Text _scoreCeilingLabel;

        #endregion

        #region Public methods

        public override void UpdatePrecalculatedScore(int preScore) { }

        public override void UpdateScore(int scoreFloor, int score, int scoreCeiling)
        {
            _scoreFloorLabel.text = scoreFloor.ToString();
            _scoreLabel.text = score.ToString();
            _scoreCeilingLabel.text = scoreCeiling.ToString();
        }

        #endregion
    }
}