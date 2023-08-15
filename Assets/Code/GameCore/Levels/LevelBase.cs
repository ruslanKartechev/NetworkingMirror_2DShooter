using Mirror;

namespace GameCore.Levels
{
    public abstract class LevelBase : NetworkBehaviour
    {
        public abstract void Pause();
        public abstract void Play();
        public abstract void Unload();
    }
}