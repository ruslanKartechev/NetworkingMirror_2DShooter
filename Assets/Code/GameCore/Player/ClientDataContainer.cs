using System;
using UnityEngine;

namespace GameCore.Player
{
    [CreateAssetMenu(menuName = "SO/" + nameof(ClientDataContainer), fileName = nameof(ClientDataContainer), order = 0)]
    public class ClientDataContainer : ScriptableObject
    {
        [SerializeField] private string _defaultName;

        public string GetDefaultName() => _defaultName;
        public string PlayerName { get; set; }
        public Color PlayerColor { get; set; } = Color.white;

        private void Awake()
        {
            PlayerName = _defaultName;
        }
    }
}