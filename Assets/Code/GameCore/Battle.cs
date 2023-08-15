using System.Collections.Generic;
using GameCore.Player;
using GameCore.Projectiles;
using Mirror;
using UnityEngine;
using Utils;

namespace GameCore
{
    [DefaultExecutionOrder(0)]
    public class Battle : NetworkBehaviour
    {
        public static Battle current => _singleton;
        private static Battle _singleton;
        
        [SerializeField] private MovementBordersSetter _bordersSetter;
        [SerializeField] private BulletsPool _pool;
        private List<PlayerClient> _currentPlayers = new List<PlayerClient>(2);
        private List<PlayerClient> _alivePlayers = new List<PlayerClient>(2);
        
        public IPool<IGunBullet> BulletPool => _pool;
        public IBorderChecker BorderChecker => _bordersSetter;

        public IList<PlayerClient> Players => _currentPlayers;

        private void Awake()
        {
            if (_singleton == null)
            {
                _singleton = this;
                return;
            }
            Destroy(this);
        }

        [Server]
        public void AddPlayer(PlayerClient client)
        {
            _currentPlayers.Add(client);
            _alivePlayers.Add(client);
        }

        [Server]
        public void OnPlayerKilled(PlayerClient client)
        {
            _alivePlayers.Remove(client);
            client.Loose();
            CLog.LogWHeader(nameof(Battle), $"Player killed, alive left: {_alivePlayers.Count}", "w");
            if (_alivePlayers.Count == 1)
            {
                CLog.LogWHeader(nameof(Battle), $"One player left, WIN", "w");
                _alivePlayers[0].Win();                
            }   
        }
          
    }
}