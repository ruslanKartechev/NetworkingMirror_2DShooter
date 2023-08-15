using NetCode;
using UnityEngine;

namespace GameCore
{
    public class ServerManager : MonoBehaviour
    {
        [SerializeField] private string _serverSceneName;
        [SerializeField] private NetworkManagerShooter _networkManager;
        [SerializeField] private PrefabsRepository _prefabsRepository;
        
#if SERVER_BUILD
        void Start()
        {
            Debug.Log($"Starting as server only");
            _prefabsRepository.Init();
            _networkManager.StartServer();
            _networkManager.ServerChangeScene(_serverSceneName);
        }  
#endif
    }
}