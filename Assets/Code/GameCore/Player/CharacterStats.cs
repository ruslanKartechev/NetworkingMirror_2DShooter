using UnityEngine;

namespace GameCore.Player
{
    [CreateAssetMenu(menuName = "SO/" + nameof(CharacterStats), fileName = nameof(CharacterStats), order = 0)]
    public class CharacterStats : ScriptableObject
    {
        public float moveSpeed;
        public float bulletDamage;
        public float bulletSpeed;
        public float fireRate;
    }
}