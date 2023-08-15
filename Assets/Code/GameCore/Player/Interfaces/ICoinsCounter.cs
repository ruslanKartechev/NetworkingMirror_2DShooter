namespace GameCore.Player
{
    public interface ICoinsCounter
    {
        void Reset();
        void Add(float count);
        float GetCount();
    }
}