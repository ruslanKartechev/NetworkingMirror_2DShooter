using System;
using System.Collections;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace GameCore.Levels
{
    public partial class SceneLevelManager : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private EditorSwitcher _editorSwitcher;
        public EditorSwitcher EditorSwitch => _editorSwitcher;
#endif
        [SerializeField] private LevelBase _loadedLevel;
        [SerializeField] private Scene _loadedScene;
        [SerializeField] private LevelsRepository _levels;
        
        public LevelsRepository Repository => _levels;
        private int CurrentIndex { get; set; }
        private int LevelsPassed => 0;
        private Action _onLoadedCall;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Init()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
#if UNITY_EDITOR
            _editorSwitcher.ClearAll();
#endif
        }

        [Server]
        public void LoadCurrent()
        {
            LoadLevel(CurrentIndex);
        }

        [Server]
        public void LoadNext()
        {
            LoadLevel(CurrentIndex + 1);
        }
        
        [Server]
        public void LoadPrev()
        {
            LoadLevel(CurrentIndex - 1);
        }

        private void LoadLevel(int levelIndex)
        {
            levelIndex = GetCorrectedIndex(levelIndex);
            CurrentIndex = levelIndex;
            var levelName = _levels.GetSceneName(levelIndex);
            LoadScene(levelName);
        }
        
        private void LoadScene(string sceneName)
        {
            UnloadPrev();
            NetworkManager.singleton.ServerChangeScene(sceneName);
            // SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StartCoroutine(DelayedCall(scene, mode));
        }

        private IEnumerator DelayedCall(Scene scene, LoadSceneMode mode)
        {
            yield return null;
            _loadedScene = scene;
            _loadedLevel = FindObjectOfType<LevelBase>();
            _onLoadedCall?.Invoke();   
        }

        private void UnloadPrev()
        {
            if(_loadedLevel != null)
                _loadedLevel.Unload();
            if(_loadedScene.name != null)
                SceneManager.UnloadSceneAsync(_loadedScene);
            _loadedLevel = null;
        }
        
        private int GetCorrectedIndex(int levelIndex)
        {
            levelIndex = Mathf.Clamp(levelIndex, 0, _levels.Count - 1);
            var totalCount = LevelsPassed;
            if (totalCount > _levels.Count - 1)
            {
                if (_levels.Count == 1)
                    return 0;
                var level = CurrentIndex;
                var startIndex = level;
                while (level == startIndex)
                {
                    level = UnityEngine.Random.Range(0, _levels.Count);
                }
                return level;
            }
            return levelIndex;
        }
    }
}