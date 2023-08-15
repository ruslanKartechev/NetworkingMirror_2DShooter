using GameCore.Player;
using Mirror;
using UnityEngine;

namespace GameCore.Obstacles
{
    public class Wall : NetworkBehaviour, IDamageable
    {
        [SerializeField] private ImageFlicker _imageFlicker;   
        
        [Server]
        public void TakeDamage(float damage)
        {
            Debug.Log("Wall take damage");
            _imageFlicker.Flick();
            RpcShowDamage();
        }

        [ClientRpc]
        private void RpcShowDamage()
        {
            _imageFlicker.Flick();
        } 
        
    }
}