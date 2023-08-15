namespace GameCore.Player
{
    public interface IPool<T>
    {
        void InitPool();
        T GetItem();
        void GiveItem(T item);
    }
}