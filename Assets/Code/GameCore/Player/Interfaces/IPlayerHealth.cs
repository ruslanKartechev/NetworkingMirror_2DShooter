using UI;

namespace GameCore.Player
{
    public interface IPlayerHealth
    {
        void SetDamageable(bool damageable);
        void SetUI(IPlayerHealthUI ui);
        public void SetDeathListener(IDeathListener listener);
        public void ShowHealthUI();
        public void HideHealthUI();

    }
}