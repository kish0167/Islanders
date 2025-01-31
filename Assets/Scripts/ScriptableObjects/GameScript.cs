using System.Collections.Generic;
using UnityEngine;

namespace Islanders.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameScript", menuName = "Script/GameScript")]
    public class GameScript : ScriptableObject
    {
        [SerializeField] private List<Step> _steps;

        public List<Step> Steps => _steps;
    }
}