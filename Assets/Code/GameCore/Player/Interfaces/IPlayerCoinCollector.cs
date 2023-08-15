using UI;

namespace GameCore.Player
{
    public interface IPlayerCoinCollector
    {
        void SetUI(ICoinsUI coinsUI);
        void SetCounter(ICoinsCounter counter);
    }
}