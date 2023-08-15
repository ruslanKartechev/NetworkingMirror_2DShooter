using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace GameCore.Player
{
    public class LevelSpawnPoints : NetworkBehaviour
    {
        [SerializeField] private Transform _spawnPointDefault;
        [SerializeField] private List<Transform> _spawnPoints;
        private int _count;
        
        public void Add()
        {
            _count++;
        }

        public int GetCount() => _count;
        
        public Transform GetSpawnPoint()
        {
            if (_count >= _spawnPoints.Count || _count < 0)
                return _spawnPointDefault;
            return _spawnPoints[_count];
        }
    }
}