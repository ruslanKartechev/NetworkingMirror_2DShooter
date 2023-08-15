using UnityEngine;

namespace GameCore
{
    public interface IBorderChecker
    {
        public bool CheckBorders(Vector3 position);
    }
}