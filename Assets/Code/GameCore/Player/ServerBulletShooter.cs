using GameCore.Projectiles;
using Mirror;

namespace GameCore.Player
{
    public class ServerBulletShooter : NetworkBehaviour
    {
        private IPool<IGunBullet> _pool;
        public override void OnStartServer()
        {
            NetworkServer.RegisterHandler<BulletFireMessage>(FireBullets);
            _pool = Battle.current.BulletPool;
            _pool.InitPool();
        }

        private void FireBullets(NetworkConnectionToClient connection, BulletFireMessage message)
        {
            var bullet = _pool.GetItem();
            bullet.Fire(message.args);
        }
    }
}