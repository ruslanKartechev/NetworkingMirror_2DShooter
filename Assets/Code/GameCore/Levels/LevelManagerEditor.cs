#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GameCore.Levels
{
    [CustomEditor(typeof(SceneLevelManager))]
    public class LevelManagerEditor : Editor
    {
        private SceneLevelManager me;

        private void OnEnable()
        {
            me = target as SceneLevelManager;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("<<", GUILayout.Width(50)))
            {
                me.EditorSwitch.Prev(me.Repository);
            }
            if (GUILayout.Button(">>", GUILayout.Width(50)))
            {
                me.EditorSwitch.Prev(me.Repository);
            }
            if (GUILayout.Button("Clear", GUILayout.Width(120)))
            {
                me.EditorSwitch.ClearAll();
            }

            GUILayout.EndHorizontal();
        }
    }
}
#endif