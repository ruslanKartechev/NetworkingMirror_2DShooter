using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(menuName = "SO/" + nameof(PrefabsRepository), fileName = nameof(PrefabsRepository), order = 0)]
    public class PrefabsRepository : ScriptableObject
    {
        public const string PlayerCharacterPrefabName = "player";
        public const string ClientPrefabName = "client";
        public const string CoinsUIPrefabName = "coinsUI";
        public const string HealthUIPrefabName = "healthUI";
        public const string BulletPrefabName = "bullet";
        public const string WinUIPrefabName = "winUI";
        public const string LooseUIPrefabName = "looseUI";
        public const string ControlsUIPrefabName = "controlsUI";
        

        public List<Data> prefabs;
        private Dictionary<string, GameObject> _prefabsByName;
        private static PrefabsRepository _instance;

        public static GameObject GetPrefab(string prefabName) => _instance.GetPrefabInt(prefabName);

        public void Init()
        {
            if (_instance != null)
            {
                Destroy(this);
                return;
            }
            _instance = this;
            _prefabsByName = new Dictionary<string, GameObject>(prefabs.Count);
            foreach (var data in prefabs)
            {
                _prefabsByName.Add(data.name, data.prefab);
                if(data.netcodeRegistered)
                    NetworkManager.singleton.spawnPrefabs.Add(data.prefab);
            }
        }


        private GameObject GetPrefabInt(string prefabName)
        {
            if (_prefabsByName.TryGetValue(prefabName, out var val))
                return val;
            Debug.Log($"[{nameof(PrefabsRepository)}] No prefab: {prefabName} found");
            return null;
        }


        [System.Serializable]
        public class Data
        {
            public GameObject prefab;
            public string name;
            public bool netcodeRegistered = true;
        }
    }
}