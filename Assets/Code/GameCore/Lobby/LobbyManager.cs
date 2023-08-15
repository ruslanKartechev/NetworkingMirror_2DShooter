using GameCore.Levels;
using GameCore.Player;
using NetCode;
using UnityEngine;
using Utils;

namespace GameCore.Lobby
{
    public class LobbyManager : MonoBehaviour, ILobbyUIListener
    {
        [SerializeField] private LobbyUI _lobbyUI;
        [SerializeField] private NetworkManagerShooter _networkManager;
        [SerializeField] private SceneLevelManager _levelManager;
        [SerializeField] private ClientDataContainer _clientData;
        // Can add options to choose a GameMode by the player
        
        public void ActivateLobby()
        {
            CLog.LogWHeader(nameof(LobbyManager), "Lobby activated", "g", "w");
            _lobbyUI.SetListener(this);
            if (string.IsNullOrEmpty(_clientData.PlayerName))
                _clientData.PlayerName = _clientData.GetDefaultName();
            _lobbyUI.Show();
        }

        public void OnSetName(string name)
        {
            _clientData.PlayerName = name;
        }

        public void OnCreateRoom(string name)
        {
            CLog.LogWHeader(nameof(LobbyManager), "Creating room", "g", "w");
            _lobbyUI.Hide();
            _networkManager.StartHost();
            _levelManager.LoadCurrent();
        }

        public void OnJoinRoom(string name)
        {
            CLog.LogWHeader(nameof(LobbyManager), "Joining room", "g", "w");
            _lobbyUI.Hide();
            _networkManager.StartClient();
        }

        public void SetColor(Color color)
        {
            _clientData.PlayerColor = color;
        }
    }
}