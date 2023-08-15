using Mirror;
using UI;
using UnityEngine;

namespace GameCore.Player
{
    public class PlayerHealth : NetworkBehaviour, IPlayerHealth, IDamageable
    {
        [SerializeField] private float _startHealth = 100f;
        [SerializeField] private float _health = 100;
        [SerializeField] private Transform _healthBarFollowTarget;
        private IPlayerHealthUI _healthUI;
        private IDeathListener _listener;
        private bool _isDamageable;

        public float Health => _health / _startHealth;
        
        private void Awake()
        {
            _health = _startHealth;
        }

        public void SetDeathListener(IDeathListener listener)
        {
            _listener = listener;
        }
        
        public void SetUI(IPlayerHealthUI ui)
        {
            Debug.Log("[Player health] Set health ui called");
            _healthUI = ui;
            _healthUI.SetFollowTarget(_healthBarFollowTarget);
        }
        
        public void SetDamageable(bool damageable)
        {
            _isDamageable = damageable;
        }
        
        public void ShowHealthUI()
        {
            _healthUI.Show(true);
            _healthUI.SetHealthNow(Health);
        }

        public void HideHealthUI()
        {
            _healthUI.Hide(true);
        }
        
        [ClientRpc]
        private void RpcShowDamaged(float remainingHealth)
        {
            _health = remainingHealth;
            _healthUI.SetHealth(Health);
        }
        
        [Server]
        public void TakeDamage(float damage)
        {
            if (!_isDamageable)
                return;
            _health -= damage;
            if (_health < 0)
                _health = 0;
            RpcShowDamaged(_health);
            if(_health == 0)
                _listener?.OnDeath();
            // CLog.LogWHeader(nameof(PlayerHealth), $"Damage {damage}, health: {_health}", "r", "w");
        }
    }
}