using Mirror;
using UI;
using UnityEngine;
using Utils;

namespace GameCore.Player
{
    public class PlayerCharacter : NetworkBehaviour    
    {
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private PlayerAppearance _appearance;
        [SerializeField] private PlayerShooter _shooter;
        [SerializeField] private CharacterStats _stats;
        private IPlayerCoinCollector _playerCoinCollector;
        private IPlayerHealth _playerHealth;

        private void Awake()
        {
            _playerCoinCollector = gameObject.GetComponent<IPlayerCoinCollector>();
            _playerHealth = gameObject.GetComponent<IPlayerHealth>();
        }

        private void Start()
        {
            _appearance.UpdateAppearance();
            if(!isServer)
                SetGoName();
        }

        [Server]
        public void SetupCharacter(string playerName, Color color, IDeathListener deathListener, ICoinsCounter counter)
        {
            gameObject.name = $"Player Character: {playerName}";
            _appearance.SetSyncVars(playerName, color);
            _playerHealth.SetDeathListener(deathListener);
            _mover.SetMoveSpeed(_stats.moveSpeed);
            _shooter.SetBulletDamage(_stats.bulletDamage);
            _shooter.SetBulletSpeed(_stats.bulletSpeed);
            _shooter.SetFireRate(1f / _stats.fireRate);
            _playerCoinCollector.SetCounter(counter);
        }

        [Server]
        public void ServerStartFighting()
        {
            _playerHealth.SetDamageable(true);
            _playerHealth.ShowHealthUI();
        }
        
        [Client]
        public void ClientStartFighting()
        {
            CLog.LogWHeader(nameof(PlayerCharacter), $"Character StartPlaying, isOwned: {isOwned}", "g","w");
            if (isOwned)
                _mover.AllowMovement();
            _shooter.AllowShooting(isOwned);
            _playerHealth.SetDamageable(true);
            _playerHealth.ShowHealthUI();
            if(_playerCoinCollector == null)
                _playerCoinCollector = gameObject.GetComponent<IPlayerCoinCollector>();
        }
        
        [Server]
        public void ServerStopFighting()
        {
            _playerHealth.SetDamageable(false);
            _playerHealth.HideHealthUI();
            RpcStopFighting();
        }
        
        public void SetMoveBorders(IBorderChecker borderChecker) => _mover.SetMoveBorders(borderChecker);

        public void SetControlsUI(IControlsUI controlsUI)
        {
            _mover.SetControlsUI(controlsUI);
            controlsUI.SetFireListener(_shooter);
        }

        public void SetHealthUI(IPlayerHealthUI healthUI)
        {
            Debug.Log($"[Character] Set health UI");
            healthUI.Hide(false);
            _playerHealth.SetUI(healthUI);
        }

        public void SetCoinsUI(ICoinsUI coinsUI) => _playerCoinCollector.SetUI(coinsUI);


        [ClientRpc]
        private void RpcStopFighting()
        {
            CLog.LogWHeader(nameof(PlayerCharacter), $"Character Stopped", "r");
            StopFighting();
        }
        
        private void StopFighting()
        {
            if (isOwned)
                _mover.StopMovement();
            _playerHealth.SetDamageable(false);
            _playerHealth.HideHealthUI();
        }


        #region Debug Gameobject name set
        private void SetGoName()
        {
            if (isOwned)
                gameObject.name = $"Local Character";
            else
                gameObject.name = $"Player Character: {UnityEngine.Random.Range(0,100)}";
        }
        #endregion
    }
}