using UnityEngine;

namespace GameCore.Projectiles
{
    public interface IGunBullet
    {
        void SetPosition(Vector3 position);
        void Fire(BulletFireArgs args);
    }
}