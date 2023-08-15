using System.Collections;
using Mirror;
using UI;
using UnityEngine;
using Utils;

namespace GameCore.Player
{
    [DefaultExecutionOrder(10)]
    public class PlayerClient : NetworkBehaviour, IDeathListener
    {
        private enum ClientState
        {
            Waiting, Playing
        }
        
        [SerializeField] private CharacterSpawner _characterSpawner;
        [SerializeField] private PlayerUISpawner _uiSpawner;
        [SyncVar] private ClientState _state = ClientState.Waiting;
        private string _playerName;
        private bool _isStarted;
        private ICoinsCounter _coinsCounter;

        public override void OnStopLocalPlayer()
        {
            Debug.Log("*** Local player stopped ***");
            base.OnStopLocalPlayer();
        }

        [Client]
        private void Start()
        {
            SetGoName();
            StartCoroutine(WaitingForClientCharacter());
        }

        [Server]
        public void InitPlayerOnServer(LevelSpawnPoints spawnPoints, string playerName, Color color)
        {
            CLog.LogWHeader(nameof(PlayerCharacter), $"Server Player setup: {playerName}. ConnectionID: {netIdentity.connectionToClient.connectionId}", "g");
            netIdentity.AssignClientAuthority(netIdentity.connectionToClient);
            _playerName = playerName;
            _coinsCounter = new CoinsCounter();
            var character = _characterSpawner.SpawnCharacter(spawnPoints);
            character.SetupCharacter(playerName, color, this, _coinsCounter);
            character.SetHealthUI(_uiSpawner.SpawnHealthUI());
            Battle.current.AddPlayer(this);
        }

        [Server]
        public void StartPlaying()
        {
            _state = ClientState.Playing;
            _characterSpawner.Character.ServerStartFighting();
            RpcGiveControl();
        }

        [Server]
        public void Win()
        {
            CLog.LogWHeader(nameof(PlayerClient) + gameObject.name, "Win", "g", "g");
            var character = _characterSpawner.Character;
            character.ServerStopFighting();
            RpcShowWin(_playerName, (int)_coinsCounter.GetCount());
        }

        [Server]
        public void Loose()
        {
            CLog.LogWHeader(nameof(PlayerClient) + gameObject.name, "Loose", "g", "g");
            _characterSpawner.Character.ServerStopFighting();
            RpcShowLoose(_playerName);
        }

        [ClientRpc]
        private void RpcGiveControl()
        {
            // CLog.LogWHeader(nameof(PlayerClient), $"RPC Give control: {gameObject.name}", "w");
            _state = ClientState.Playing;
            StartCoroutine(WaitingToGiveControl());
        }
        
        [ClientRpc]
        private void RpcShowWin(string playerName, int coinsCount)
        {
            if (isOwned == false)
                return;
            var ui = _uiSpawner.SpawnWinUI();
            ui.Show(playerName, coinsCount);
        }

        [ClientRpc]
        private void RpcShowLoose(string playerName)
        {
            if (isOwned == false)
                return;
            var ui = _uiSpawner.SpawnLooseUI();
            ui.Show(playerName);
        }

        [Client]
        private IEnumerator WaitingForClientCharacter()
        {
            while (GetClientCharacter() == null)
                yield return null;
            while (NetworkClient.spawned.ContainsKey(_uiSpawner.HealthUINetId) == false)
                yield return null;
            if(isLocalPlayer)
                SetLocalPlayer();
            else
                SetNonLocalPlayer();
            _isStarted = true;
            CLog.LogWHeader(nameof(PlayerClient), $"Started Set true {gameObject.name}", "g");
        }
        
        [Client]
        private IEnumerator WaitingToGiveControl()
        {
            while (_isStarted == false)
                yield return null;
            GetClientCharacter().ClientStartFighting();
        }
        
        [Client]
        private void SetLocalPlayer()
        {
            CLog.LogWHeader(nameof(PlayerClient), $"[{gameObject.name}] Setting LOCAL player", "w");
            var character = GetClientCharacter();
            character.SetCoinsUI(_uiSpawner.SpawnCoinsUI());
            character.SetHealthUI(NetworkClient.spawned[_uiSpawner.HealthUINetId].GetComponent<IPlayerHealthUI>());
            character.SetControlsUI(_uiSpawner.SpawnControlsUI());
            character.SetMoveBorders(Battle.current.BorderChecker);
        }
        
        [Client]
        private void SetNonLocalPlayer()
        {
            CLog.LogWHeader(nameof(PlayerClient), $"[{gameObject.name}] Setting NON-local player", "w");
            var character = GetClientCharacter();
            character.SetHealthUI(NetworkClient.spawned[_uiSpawner.HealthUINetId].GetComponent<IPlayerHealthUI>());
            if (_state == ClientState.Playing)
                GetClientCharacter().ClientStartFighting();
        }

        [Client]
        private PlayerCharacter GetClientCharacter()
        {
            if (isServer)
                return _characterSpawner.Character;
            if(NetworkClient.spawned.ContainsKey(_characterSpawner.CharacterNetId) == false)
                return null;
            return NetworkClient.spawned[_characterSpawner.CharacterNetId].GetComponent<PlayerCharacter>();
        }

        public void OnDeath()
        {
            CLog.LogWHeader(nameof(PlayerClient), $"[{gameObject.name}] Client on death", "w");
            Battle.current.OnPlayerKilled(this);
        }
        
        private void SetGoName()
        {
            if (isServer)
                return;
            if (isOwned)
                gameObject.name = $"Local Client";
            else
                gameObject.name = $"Client: {UnityEngine.Random.Range(0,100)}";
        }

    }
}