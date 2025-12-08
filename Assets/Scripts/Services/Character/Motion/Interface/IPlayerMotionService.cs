using UnityEngine;

namespace Services.Character.Motion
{
    public interface IPlayerMotionService
    {
        public void UpdateTransform(Vector2 direction);
    }
}