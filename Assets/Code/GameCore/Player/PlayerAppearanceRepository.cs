using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace GameCore.Player
{
    [CreateAssetMenu(menuName = "SO/" + nameof(PlayerAppearanceRepository), fileName = nameof(PlayerAppearanceRepository), order = 0)]
    public class PlayerAppearanceRepository : ScriptableObject
    {
        public List<string> possibleNames;
        public List<Color> possibleColors ;

        public string GetRandomName()
        {
            return possibleNames.Random();
        }
        
        public int GetRandomColorIndex()
        {
            return possibleColors.RandomIndex();
        }

        public Color GetColor(int index) => possibleColors[index];
        public int ColorsCount => possibleColors.Count;
    }
    
}