using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Islanders.Game.UI.ScoreBox
{
    public class RadialScoreBox : ScoreBox
    {
        #region Variables

        [SerializeField] private Image _scoreCircle;
        [SerializeField] private Image _preScoreCircle;
        [SerializeField] private TMP_Text _scoreLabel;
        private int _score;
        private int _scoreCeiling;
        private int _scoreFloor;

        #endregion

        #region Public methods

        public override void UpdatePrecalculatedScore(int preScore)
        {
            _preScoreCircle.fillAmount = CalculateFillAmount(preScore + _score);
        }

        public override void UpdateScore(int scoreFloor, int score, int scoreCeiling)
        {
            _scoreFloor = scoreFloor;
            _scoreCeiling = scoreCeiling;
            _score = score;

            _scoreCircle.fillAmount = CalculateFillAmount(score);
            _scoreLabel.text = $"{score}/{scoreCeiling}";
        }

        #endregion

        #region Private methods

        private float CalculateFillAmount(float value)
        {
            float fillAmount = (value - _scoreFloor) / (_scoreCeiling - _scoreFloor);
            return Math.Clamp(fillAmount, 0f, 1f);
        }

        #endregion
    }
}