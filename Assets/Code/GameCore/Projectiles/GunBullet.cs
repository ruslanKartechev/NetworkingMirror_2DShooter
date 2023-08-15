using System.Collections;
using GameCore.Player;
using Mirror;
using UnityEngine;

namespace GameCore.Projectiles
{
    [DefaultExecutionOrder(100)]
    public class GunBullet : NetworkBehaviour, IGunBullet, IPooledObject<IGunBullet>
    {
        private const float MaxDistance = 15;
        [SerializeField] private Transform _movable;
        [SerializeField] private PlayerSpritePainter _painter;
        [SerializeField] private Collider2D _collider;
        private IPool<IGunBullet> _pool;
        private float _damage;
        private uint _senderID;
        private Coroutine _movinig;

        public override void OnStartClient()
        {
        }
        
        public void SetColor(Color color)
        {
            _painter.SetColor(color);
        }

        public void SetPosition(Vector3 position)
        {
            _movable.position = position;
        }

        [Server]
        public void Fire(BulletFireArgs args)
        {
            _senderID = args.senderID;
            if (!isClient && isServer)
                Launch(args);
            RpcFire(args);
        }

        [ClientRpc]
        private void RpcFire(BulletFireArgs args)
        {
            Launch(args);
        }

        private void Launch(BulletFireArgs args)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, args.angle);
            _movable.position = args.startPosition;
            _collider.enabled = true;
            _damage = args.damage;
            SetColor(args.color);
            gameObject.SetActive(true);
            if(_movinig != null)
                StopCoroutine(_movinig);
            _movinig = StartCoroutine(Moving(args.direction, args.speed));
        }
        
        public void SetPool(IPool<IGunBullet> pool)
        {
            _pool = pool;
        }

        [Server]
        private void ResetServer()
        {
            RpcReset();
            _damage = 0f;
            _movable.position = new Vector3(-100, -100, 0f);
            // gameObject.SetActive(false);
            _pool.GiveItem(this);
        }
        
        [ClientRpc]
        private void RpcReset()
        {
            if (isServer)
                return;
            _damage = 0f;
            _movable.position = new Vector3(-100, -100, 0f);
            // gameObject.SetActive(false);
        }

        private IEnumerator Moving(Vector3 direction, float speed)
        {
            var totalDistance = 0f;
            while (true)
            {
                var travelled = (Time.deltaTime * speed);
                _movable.position += direction * travelled;
                totalDistance += travelled;
                if (totalDistance >= MaxDistance)
                {
                    ResetServer();
                    break;
                }
                yield return null;
            }
        }
        
        [Server]
        private void OnTriggerEnter2D(Collider2D other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable == null)
                return;
            if (other.GetComponent<NetworkIdentity>().assetId == _senderID)
                return;
            damageable.TakeDamage(_damage);
            ResetServer();
        }
    }
}