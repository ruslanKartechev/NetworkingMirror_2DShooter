#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
#endif

namespace GameCore.Levels
{
#if UNITY_EDITOR
    public partial class SceneLevelManager
    {
        [System.Serializable]
        public class EditorSwitcher
        {
            public int currentIndex;
            public Scene currentScene;
            
            
            public void Next(LevelsRepository levels)
            {
                currentIndex = Correct(currentIndex + 1, levels.Count - 1);
                Unload();
                Load(levels.GetSceneName(currentIndex));
            }

            public void Prev(LevelsRepository levels)
            {
                currentIndex = Correct(currentIndex - 1, levels.Count - 1);
                Unload();
                Load(levels.GetSceneName(currentIndex));
            }
            
            
            private int Correct(int index, int max)
            {
                return Mathf.Clamp(index,0, max);
            }

            public void Unload()
            {
                var count = EditorSceneManager.sceneCount;
                for (int i = count-1; i >= 1; i--)
                {
                    var scene = EditorSceneManager.GetSceneAt(i);
                    EditorSceneManager.CloseScene(scene, true);
                }
                if(currentScene.name != null) 
                    EditorSceneManager.CloseScene(currentScene, true);
            }

            public void Reset()
            {
                currentIndex = 0;
            }
            
            public void ClearAll()
            {
                var count = SceneManager.sceneCount;
                for (int i = count-1; i >= 1; i--)
                {
                    var scene = SceneManager.GetSceneAt(i);
                    SceneManager.UnloadSceneAsync(scene);
                }
            }

            private string GetPath(string name)
            {
                return "Assets/Scenes/" + name + ".unity";
            }

            private void Load(string name)
            {
                currentScene = EditorSceneManager.OpenScene(GetPath(name), OpenSceneMode.Additive);
            }
            
        }
    }
#endif
}
