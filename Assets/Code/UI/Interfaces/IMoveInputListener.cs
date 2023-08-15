using UnityEngine;

namespace UI
{
    public interface IMoveInputListener
    {
        void Move(Vector2 direction, float magnitude);
    }
}