using Mirror;
using UnityEngine;

namespace GameCore
{
    public struct SpawnClientMessage : NetworkMessage
    {
        public string name;
        public Color color;
    }
}