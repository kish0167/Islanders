using System.Collections.Generic;
using UnityEngine;

namespace Islanders.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameScript", menuName = "Script/GameScript")]
    public class GameScript : ScriptableObject
    {
        [SerializeField] public List<Step> _steps;
    }
}