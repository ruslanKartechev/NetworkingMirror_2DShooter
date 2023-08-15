using Mirror;
using UnityEngine;

namespace UI
{
    public class BattleManagerUI : NetworkBehaviour
    {
        [SerializeField] private GameObject _waitingBlock;

        public void ShowWaiting()
        {
            
        }
    }

    public class WaitingBlockUI : NetworkBehaviour
    {
        
    }
}
