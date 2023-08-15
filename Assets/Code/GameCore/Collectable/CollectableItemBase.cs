using Mirror;

namespace GameCore.Collectable
{
    public abstract class CollectableItemBase : NetworkBehaviour
    {
        public abstract bool IsPickable();
        public abstract void Collect();
        
    }
}