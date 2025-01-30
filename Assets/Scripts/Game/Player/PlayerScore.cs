using Islanders.Game.ScoreHandling;
using Islanders.Game.UI;
using Zenject;

namespace Islanders.Game.Player
{
    public class PlayerScore
    {
        private int _score = 0;
        private int _scoreFloor = 0;
        private int _scoreCeiling = 0;

        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                UpdateUI();
            }
        }

        public int ScoreFloor
        {
            get => _scoreFloor;
            private set
            {
                _scoreFloor = value;
                UpdateUI();
            }
        }

        public int ScoreCeiling
        {
            get => _scoreCeiling;
            private set
            {
                _scoreCeiling = value;
                UpdateUI();
            }
        }
        
        private ScoreBox _box;
        [Inject]
        public PlayerScore(ScoreBox box)
        {
            _box = box;
            ScoreCounter.OnScoreAcquiring += ScoreAcquiringCallback;
        }
        

        private void ScoreAcquiringCallback(int quantity)
        {
            Score += quantity;
        }

        private void UpdateUI()
        {
            _box.UpdateLabels(_scoreFloor, _score, _scoreCeiling);
        }

        #region Public methods

        public void SetToZero()
        {
            Score = 0;
            ScoreFloor = 0;
            ScoreCeiling = 0;
        }

        #endregion
    }
}