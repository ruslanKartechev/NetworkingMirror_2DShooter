namespace GameCore.Player
{
    public class CoinsCounter: ICoinsCounter
    {
        private float _count;

        public void Reset()
        {
            _count = 0f;
        }

        public void Add(float count)
        {
            _count += count;
        }

        public float GetCount()
        {
            return _count;
        }
    }
}