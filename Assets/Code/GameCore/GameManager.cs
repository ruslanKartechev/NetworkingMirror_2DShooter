using GameCore.Lobby;
using NetCode;
using UnityEngine;

namespace GameCore
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LobbyManager _lobbyManager;
        [SerializeField] private NetworkManagerShooter _networkManager;
        [SerializeField] private PrefabsRepository _prefabsRepository;

#if !SERVER_BUILD
        private void Awake()
        {
            SRDebug.Init();
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            _prefabsRepository.Init();
            _lobbyManager.ActivateLobby();
        }
#endif

    }

    /*
     The built-in callbacks are:
        OnStartServer | called on server when a game object spawns on the server, or when the server is started for game objects in the Scene
        OnStopServer | called on server when a game object is destroyed on the server, or when the server is stopped for game objects in the Scene
        OnStartClient | called on clients when the game object spawns on the client, or when the client connects to a server for game objects in the Scene
        OnStopClient | called on clients when the server destroys the game object
        OnStartLocalPlayer | called on clients after OnStartClient for the player game object on the local client
        OnStopLocalPlayer | called on clients before OnStopClient for the player game object on the local client
        OnStartAuthority | called on owner client when assigned authority by the server. isOwned will be true for such objects in client context.
        OnStopAuthority | called on owner client when authority is removed by the server.
     */
}

