using Mirror;
using TMPro;
using UnityEngine;
using Utils;

namespace GameCore.Player
{
    public class PlayerAppearance : NetworkBehaviour
    {
        [SerializeField] private TextMeshPro _nameText;
        [SerializeField] private PlayerSpritePainter _painter;
        [SyncVar] private Color _color = Color.green;
        [SyncVar] private string _playerName = "Unset";

        public Color MainColor => _color;
        public string PlayerName => _playerName;

        public void SetNameVar(string name)
        {
            _playerName = name;
        }
        
        public void SetColorVar(Color color)
        {
            _color = color;
        }

        public void SetSyncVars(string name, Color color)
        {
            SetNameVar(name);
            SetColorVar(color);
        }
        
        public void UpdateAppearance()
        {
            _painter.SetColor(_color);
            _nameText.text = _playerName;
        }
        //
        // public void UpdateAppearanceClient()
        // {
        //     CLog.LogWHeader(nameof(PlayerAppearance), $"Update appearance client, sync color: {_color}, sync name: {_name}", "w");
        //     UpdateAppearance();
        // }

    }
}