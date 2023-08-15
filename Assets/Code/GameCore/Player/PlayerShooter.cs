using System.Collections;
using GameCore.Projectiles;
using Mirror;
using UnityEngine;

namespace GameCore.Player
{
    public class PlayerShooter : NetworkBehaviour, IFireInputListener
    {
        [SyncVar] [SerializeField] private float _bulletSpeed;
        [SyncVar] [SerializeField] private float _bulletDamage;
        [SyncVar] [SerializeField] private float _fireDelay;
        
        [SerializeField] private Transform _shootFromPoint;
        [SerializeField] private Transform _direction;
        [SerializeField] private PlayerAppearance _appearance;
        private Coroutine _shootingInput;
        private double _lastFireTime;

        public void SetBulletSpeed(float speed) => _bulletSpeed = speed;
        public void SetBulletDamage(float damage) => _bulletDamage = damage;
        public void SetFireRate(float fireDelay) => _fireDelay = fireDelay;
        
        private bool _allowed;
        public void AllowShooting(bool allow)
        {
            _allowed = allow;
            if(_shootingInput != null)
                StopCoroutine(InputCheck());
            if (allow)
                _shootingInput = StartCoroutine(InputCheck());
        }

        #region For tests on keyboard
        private IEnumerator InputCheck()
        {
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    Fire();
                yield return null;
            }
        }
        #endregion


        public void Fire()
        {
            if (!_allowed)
                return;
            if (Time.timeAsDouble - _lastFireTime < _fireDelay)
                return;
            _lastFireTime = Time.timeAsDouble;
            var args = new BulletFireArgs(_shootFromPoint.position, _direction.up, _bulletSpeed, 
                _bulletDamage, _direction.eulerAngles.z, _appearance.MainColor, netIdentity.assetId);
            
            NetworkClient.Send(new BulletFireMessage(args));
            
        }

    }
}