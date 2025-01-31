using Islanders.Game.ScoreHandling;
using Islanders.Game.UI;
using Zenject;

namespace Islanders.Game.Player
{
    public class PlayerScore
    {
        #region Variables

        private readonly ScoreBox _box;
        private int _score;
        private int _scoreCeiling;
        private int _scoreFloor;

        #endregion

        #region Properties

        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
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

        public int ScoreFloor
        {
            get => _scoreFloor;
            private set
            {
                _scoreFloor = value;
                UpdateUI();
            }
        }

        #endregion

        #region Setup/Teardown

        [Inject]
        public PlayerScore(ScoreBox box)
        {
            _box = box;
            ScoreCounter.OnScoreAcquiring += ScoreAcquiringCallback;
        }

        #endregion

        #region Public methods

        public void SetToZero()
        {
            Score = 0;
            ScoreFloor = 0;
            ScoreCeiling = 0;
        }

        #endregion

        #region Private methods

        private void ScoreAcquiringCallback(int quantity)
        {
            Score += quantity;
        }

        private void UpdateUI()
        {
            _box.UpdateLabels(_scoreFloor, _score, _scoreCeiling);
        }

        #endregion
    }
}