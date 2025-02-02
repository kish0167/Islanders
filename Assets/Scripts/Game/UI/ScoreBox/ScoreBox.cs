using UnityEngine;

namespace Islanders.Game.UI.ScoreBox
{
    public abstract class ScoreBox : MonoBehaviour
    {
        public abstract void UpdateScore(int scoreFloor, int score, int scoreCeiling);
        public abstract void UpdatePrecalculatedScore(int preScore);
    }
}