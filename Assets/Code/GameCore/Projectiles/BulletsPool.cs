using System.Collections.Generic;
using GameCore.Player;
using Mirror;
using UnityEngine;

namespace GameCore.Projectiles
{
    public class BulletsPool : NetworkBehaviour, IPool<IGunBullet>
    {
        [SerializeField] private int _count = 30;
        private Queue<IGunBullet> _bullets = new Queue<IGunBullet>();
        private int _spawnedCount;
        
        public void InitPool()
        {
            // CLog.LogWHeader(nameof(BulletsPool), $"Bullets pool init", "y");
            var prefab = PrefabsRepository.GetPrefab(PrefabsRepository.BulletPrefabName);
            var parent = new GameObject("Bullets Pool Parent");
            for (var i = 0; i < _count; i++)
            {
                _spawnedCount++;
                var instance = Instantiate(prefab, new Vector3(-100,- 100, 0f), Quaternion.identity, parent.transform);
                instance.GetComponent<IPooledObject<IGunBullet>>().SetPool(this);
                instance.gameObject.name = $"Bullet {_spawnedCount}";
                _bullets.Enqueue(instance.GetComponent<IGunBullet>());
                NetworkServer.Spawn(instance);
            }
        }

        public IGunBullet GetItem()
        {
            if(_bullets.Count <= 1)
                InitPool();
            return _bullets.Dequeue();
        }

        public void GiveItem(IGunBullet item)
        {
            _bullets.Enqueue(item);
        }
    }
}