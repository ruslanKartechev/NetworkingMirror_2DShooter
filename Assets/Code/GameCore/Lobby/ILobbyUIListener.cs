using UnityEngine;

namespace GameCore.Lobby
{
    public interface ILobbyUIListener
    {
        void OnSetName(string name);
        void OnCreateRoom(string name);
        void OnJoinRoom(string name);
        void SetColor(Color color);
    }
}