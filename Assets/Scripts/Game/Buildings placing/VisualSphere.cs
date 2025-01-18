using Lean.Pool;
using UnityEngine;
using Zenject;

namespace Islanders.Game.Buildings_placing
{
    public class VisualSphere : MonoBehaviour
    {
        public void Dispose()
        {
            LeanPool.Despawn(this);
        } 
    }
}