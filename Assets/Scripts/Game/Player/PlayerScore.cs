using Islanders.Game.Buildings_placing;
using Islanders.Game.ScoreHandling;
using Islanders.Game.UI;
using Islanders.Game.UI.Hotbar;
using Islanders.Game.UI.ScoreBox;
using Islanders.Game.Undo;
using Zenject;

namespace Islanders.Game.Player
{
    public class PlayerScore
    {
        #region Variables

        private readonly ScoreBox _box;
        private readonly HotBar _hotBar;
        private readonly UndoService _undoService;
        private int _score;
        private int _scoreCeiling;
        private int _scoreFloor;

        #endregion

        #region Properties

        public int Score
        {
            get => _score;
            set
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
        public PlayerScore(ScoreBox box, HotBar hotBar, UndoService undoService)
        {
            _box = box;
            _hotBar = hotBar;
            _undoService = undoService;
            _undoService.OnPlacingUndone += PlacingUndoneCallback;
            ScoreCounter.OnScoreAcquiring += ScoreAcquiringCallback;
        }

        #endregion

        #region Public methods

        public void SetNewScoreGoal(int scoreToPass)
        {
            ScoreFloor = ScoreCeiling;
            ScoreCeiling += scoreToPass;
        }

        public void SetToZero()
        {
            Score = 0;
            ScoreFloor = 0;
            ScoreCeiling = 0;
        }

        #endregion

        #region Private methods

        private void PlacingUndoneCallback(int score, PlaceableObject arg2)
        {
            Score -= score;
        }

        private void ScoreAcquiringCallback(int quantity)
        {
            Score += quantity;
        }

        private void UpdateUI()
        {
            _box.UpdateLabels(_scoreFloor, _score, _scoreCeiling);

            if (Score >= ScoreCeiling)
            {
                _hotBar.ShowNewBuildingsButton();
            }
            else
            {
                _hotBar.HideNewBuildingsButton();
            }
        }

        #endregion
    }
}