using UnityEngine;

namespace UI
{
    public interface IPlayerHealthUI
    {
        void SetFollowTarget(Transform target);
        void StopFollowing();
        void Show(bool animated);
        void Hide(bool animated);
        void SetHealth(float health);
        void SetHealthNow(float health);
        
    }
}