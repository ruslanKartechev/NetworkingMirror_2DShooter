using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace GameCore.Levels
{
    [CreateAssetMenu(menuName = "SO/" + nameof(LevelsRepository), fileName = nameof(LevelsRepository), order = 0)]
    public class LevelsRepository : ScriptableObject
    {
        [SerializeField] private List<string> _sceneNames;

        public int Count => _sceneNames.Count;

        public string GetSceneName(int levelIndex) => _sceneNames[levelIndex];
        
    }
}