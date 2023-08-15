using GameCore.Collectable;
using Mirror;
using UI;
using UnityEngine;
using Utils;

namespace GameCore.Player
{
    public class PlayerCoinCollector : NetworkBehaviour, IPlayerCoinCollector
    {
        private ICoinsUI _coinsUI;
        private ICoinsCounter _counter;

        public void SetUI(ICoinsUI coinsUI)
        {
            _coinsUI = coinsUI;
            _coinsUI.SetCount(0);
        }

        public void SetCounter(ICoinsCounter counter)
        {
            _counter = counter;
        }

        [Server]
        private void CollectCoin(CoinBase coin)
        {
            var amount = coin.GetAmount();
            _counter.Add(amount);
            coin.Collect();
            RpcSetCoins(_counter.GetCount());
        }

        [ClientRpc]
        private void RpcSetCoins(float totalCoins)
        {
            if (!isOwned)
                return;
            _coinsUI.UpdateCoins(totalCoins);
        }

        [Server]
        private void OnTriggerEnter2D(Collider2D col)
        {
            var collectable = col.gameObject.GetComponent<CollectableItemBase>();
            if (collectable == null || collectable.IsPickable() == false)
                return;
            switch (collectable)
            {
                case CoinBase coin:
                    CollectCoin(coin);
                    break;
                default:
                    CLog.LogWHeader(nameof(PlayerCoinCollector), "Unknown type of collectable", "g","w");
                    break;
            }
        }
    }
}