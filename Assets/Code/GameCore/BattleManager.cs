using System.Collections.Generic;
using GameCore.Player;
using Mirror;
using NetCode;
using UnityEngine;
using Utils;

namespace GameCore
{
    public class BattleManager : NetworkBehaviour
    {
        [SerializeField] private int _playersMinCount = 2;
        [SerializeField] private int _playersMaxCount = 4;
        [SerializeField] private LevelSpawnPoints _spawnPoints;
        [SerializeField] private ClientDataContainer _clientData;
        private int _playersCount;
        private List<PlayerClient> _addedPlayers = new List<PlayerClient>();

        private void Awake()
        {
            if(isClient)
                SetupClient();
            if(isServer)
                SetupServer();
        }

        private void OnEnable()
        {
            if (!isClient && !isServer)
            {
                NetworkManagerShooter.singleton.EClientSceneLoaded += SetupClient;
                NetworkManagerShooter.singleton.EServerSceneLoaded += SetupServer;
            }   
        }

        private void OnDisable()
        {
            NetworkManagerShooter.singleton.EClientSceneLoaded -= SetupClient;
            NetworkManagerShooter.singleton.EServerSceneLoaded -= SetupServer;
        }

        [Server]
        private void SpawnClientPlayer(NetworkConnectionToClient connection, SpawnClientMessage message)
        {
            if (_playersCount >= _playersMaxCount)
            {
                Debug.Log($"Max players count reached {_playersMaxCount}, will not spawn new player");
                return;
            }
            CLog.LogWHeader(nameof(BattleManager), "Server SpawnClientPlayer", "b", "w");
            var playerGo = Instantiate(PrefabsRepository.GetPrefab(PrefabsRepository.ClientPrefabName));
            playerGo.name = $"Player client: {_playersCount + 1}";
            NetworkServer.AddPlayerForConnection(connection, playerGo);
            var playerInstance = playerGo.GetComponent<PlayerClient>();
            _addedPlayers.Add(playerInstance);
            playerInstance.InitPlayerOnServer(_spawnPoints, message.name, message.color);
            playerInstance.StartPlaying();
            _playersCount++;
            if (_playersCount > _playersMinCount)
            {
                Debug.Log($"Enough players to start. Count: {_playersCount}");
            }
            else
            {
                Debug.Log($"Too few players to start. Count: {_playersCount}");
            }
            // RpcSpawnPlayer(playerGo.GetComponent<NetworkIdentity>().netId);
            // if (_playersCount ==     _playersMaxCount)
            // {
            //     // CLog.LogWHeader(nameof(GameMode), "Max players joined", "r");
            //     _level.SetPlayers(_addedPlayers);
            //     _level.Play();
            // }
        }

        [Server]
        private void SetupServer()
        {
            CLog.LogWHeader(nameof(BattleManager), "Setup Server", "b", "y");
            NetworkServer.RegisterHandler<SpawnClientMessage>(SpawnClientPlayer);
        }

        [ClientCallback]
        private void SetupClient()
        {
            CLog.LogWHeader(nameof(BattleManager), "Setup Client", "g", "y");
            var message = new SpawnClientMessage()
            {
                name = _clientData.PlayerName,
                color = _clientData.PlayerColor
            };
            NetworkClient.Send(message);
        }
        
    }
}