#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Cheats
{
    [CustomEditor(typeof(BattleCheat))]
    public class BattleCheatEditor : Editor
    {
        private BattleCheat me;
        private void OnEnable()
        {
            me = target as BattleCheat;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Kill 1st", GUILayout.Width(100)))
            {
                me.Kill(0);   
            }         
            if (GUILayout.Button("Kill 2nd", GUILayout.Width(100)))
            {
                me.Kill(1);   
            }         
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Win 1st", GUILayout.Width(100)))
            {
                me.FirstWin();   
            }
            if (GUILayout.Button("Win 2nd", GUILayout.Width(100)))
            {
                me.SecondWin();
            }
            GUILayout.EndHorizontal();
        }
    }
}
#endif