using System.Collections.Generic;

namespace Utils
{
    public static class Extensions
    {
        public static T Random<T>(this IList<T> list)
        {
            switch (list.Count)
            {
                case 0: return default;
                case 1: return list[0];
                default:
                    return list[UnityEngine.Random.Range(0, list.Count - 1)];
            }
        }
        
        public static int RandomIndex<T>(this IList<T> list)
        {
            switch (list.Count)
            {
                case 0: return -1;
                case 1: return 0;
                default:
                    return UnityEngine.Random.Range(0, list.Count - 1);
            }
        }
    }
}