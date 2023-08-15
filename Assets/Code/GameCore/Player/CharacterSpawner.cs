using Mirror;
using UnityEngine;

namespace GameCore.Player
{
    public class CharacterSpawner : NetworkBehaviour
    {
        [SyncVar] private uint _characterNetId;
        private PlayerCharacter _character;
        
        public uint CharacterNetId => _characterNetId;
        public PlayerCharacter Character => _character;
        
        
        [Server]
        public PlayerCharacter SpawnCharacter(LevelSpawnPoints spawnPoints)
        {
            var spawnPoint = spawnPoints.GetSpawnPoint();
            var prefab = PrefabsRepository.GetPrefab(PrefabsRepository.PlayerCharacterPrefabName);
            var instanceGo = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            var instance = instanceGo.GetComponent<PlayerCharacter>();
            instance.gameObject.name = $"Player {spawnPoints.GetCount()}";
            NetworkServer.Spawn(instanceGo, netIdentity.connectionToClient);
            instance.netIdentity.AssignClientAuthority(netIdentity.connectionToClient);
            _characterNetId = instanceGo.GetComponent<NetworkIdentity>().netId;
            spawnPoints.Add();
            _character = instance;
            return instance;
        }

 
    }
}