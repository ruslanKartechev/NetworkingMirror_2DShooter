using GameCore;
using Mirror;

namespace UI
{
    public class PlayerUISpawner : NetworkBehaviour
    {
        [SyncVar] private uint _healthUINetId;
        public uint HealthUINetId => _healthUINetId;
        
        public ICoinsUI SpawnCoinsUI()
        {
            var instance = Instantiate(PrefabsRepository.GetPrefab(PrefabsRepository.CoinsUIPrefabName)).GetComponent<ICoinsUI>();
            return instance;
        }
        
        [Server]
        public IPlayerHealthUI SpawnHealthUI()
        {
            var instance = Instantiate(PrefabsRepository.GetPrefab(PrefabsRepository.HealthUIPrefabName));
            NetworkServer.Spawn(instance);
            _healthUINetId = instance.GetComponent<NetworkIdentity>().netId;
            return instance.GetComponent<IPlayerHealthUI>();
        }


        public IPlayerWinUI SpawnWinUI()
        {
            var instance = Instantiate(PrefabsRepository.GetPrefab(PrefabsRepository.WinUIPrefabName));
            return instance.GetComponent<IPlayerWinUI>();
        }
        
        public IPlayerLooseUI SpawnLooseUI()
        {
            var instance = Instantiate(PrefabsRepository.GetPrefab(PrefabsRepository.LooseUIPrefabName));
            return instance.GetComponent<IPlayerLooseUI>();
        }

        public IControlsUI SpawnControlsUI()
        {
            var instance = Instantiate(PrefabsRepository.GetPrefab(PrefabsRepository.ControlsUIPrefabName));
            return instance.GetComponent<IControlsUI>();   
        }
    }
}