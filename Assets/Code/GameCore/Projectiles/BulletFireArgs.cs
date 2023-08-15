using UnityEngine;

namespace GameCore.Projectiles
{
    public struct BulletFireArgs
    {
        public Vector3 startPosition;
        public Vector3 direction;
        public float speed;
        public float damage;
        public float angle;
        public Color color;
        public uint senderID;
        
        public BulletFireArgs(Vector3 startPosition, 
            Vector3 direction, 
            float speed, 
            float damage, 
            float angle, 
            Color color,
            uint senderID)
        {
            this.startPosition = startPosition;
            this.direction = direction;
            this.speed = speed;
            this.damage = damage;
            this.angle = angle;
            this.color = color;
            this.senderID = senderID;
        }
    }
}