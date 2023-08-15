namespace GameCore.Player
{
    public interface IPooledObject<T>
    {
        void SetPool(IPool<T> pool);
    }
}