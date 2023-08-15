using UnityEngine;

namespace UI
{
    public class PlayerInGameUI : MonoBehaviour
    {
        [SerializeField] private CoinsUI _coinsUI;
        [SerializeField] private PlayerHealthUI _healthUI;

        public ICoinsUI GetCoinsUI() => _coinsUI;
        public IPlayerHealthUI GetHealthUI() => _healthUI;
    }
}