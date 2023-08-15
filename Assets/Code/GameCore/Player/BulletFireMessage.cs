using GameCore.Projectiles;
using Mirror;

namespace GameCore.Player
{
    public struct BulletFireMessage : NetworkMessage
    {
        public BulletFireArgs args;

        public BulletFireMessage(BulletFireArgs args)
        {
            this.args = args;
        }
    }
}