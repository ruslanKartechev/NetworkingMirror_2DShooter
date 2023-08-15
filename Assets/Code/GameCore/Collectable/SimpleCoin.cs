using System.Collections;
using Mirror;
using UnityEngine;
using Utils;

namespace GameCore.Collectable
{
    public class SimpleCoin : CoinBase
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private float _amount = 1;
        [SerializeField] private bool _isPickable = true;
        [SerializeField] private float _scaleTime = 0.25f;

        public override bool IsPickable() => _isPickable;
        public override float GetAmount() => _amount;
        
        [Server]
        public override void Collect()
        {
            SetNonpickable();
            RpcCollect();
            if(!isClient)
                gameObject.SetActive(false);
        }

        private void SetNonpickable()
        {
            _collider.enabled = false;
            _isPickable = false;
        }

        [ClientRpc]
        private void RpcCollect()
        {
            if (isOwned)
                return;
            SetNonpickable();
            StartCoroutine(ScalingDown());
        }
        

        private IEnumerator ScalingDown()
        {
            var start = 1f;
            var end = 0f;
            var elapsed = 0f;
            var time = _scaleTime;
            while (elapsed < time)
            {
                transform.localScale = Vector3.one * Mathf.Lerp(start, end, elapsed / time);
                elapsed += Time.deltaTime;
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}