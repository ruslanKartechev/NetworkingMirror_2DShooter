using System.Collections;
using DG.Tweening;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerHealthUI : NetworkBehaviour, IPlayerHealthUI
    {
        private const float ScaleTime = 0.25f;
        [SerializeField] private Transform _movable;
        [SerializeField] private GameObject _block;
        [SerializeField] private Image _fillImage;
        [SerializeField] private float _changeTime;
        private Transform _followTarget;
        private Coroutine _changing;
        private Coroutine _following;
        private Camera _cam;
        
        public void SetFollowTarget(Transform target)
        {
            Debug.Log("[HealthUI] set follow target");
            _followTarget = target;
        }

        public void StopFollowing()
        {
            if(_following != null)
                StopCoroutine(_following);
        }

        public void StartFollowing()
        {
            StopFollowing();
            _following = StartCoroutine(Following());
        }
        
        public void Show(bool animated = false)
        {
            _cam = Camera.main;
            _block.SetActive(true);
            StartFollowing();
            if (animated)
            {
                _movable.localScale = Vector3.one * 0.1f;
                _movable.DOScale(Vector3.one, ScaleTime);
                return;
            }
            _block.SetActive(false);
            if(_following != null)
                StopCoroutine(_following);
        }
        
        public void Hide(bool animated = false)
        {
            StopFollowing();
            if (animated)
            {
                _movable.DOScale(Vector3.zero, ScaleTime).OnComplete(() =>
                {
                    _block.SetActive(false);
                });
                return;
            }
            _block.SetActive(false);
        }
        
        public void SetHealth(float health)
        {
            StopChanging();
            _changing = StartCoroutine(Changing(_fillImage.fillAmount, health));
        }

        public void SetHealthNow(float health)
        {
            StopChanging();
            _fillImage.fillAmount = health;
        }

        private void StopChanging()
        {
            if (_changing != null)
                StopCoroutine(_changing);
        }

        private IEnumerator Following()
        {
            while (true)
            {
                _movable.position = _cam.WorldToScreenPoint(_followTarget.position);
                yield return null;
            }
        }

        private IEnumerator Changing(float from, float to)
        {
            var elapsed = 0f;
            while (elapsed < _changeTime)
            {
                _fillImage.fillAmount = Mathf.Lerp(from, to, elapsed / _changeTime);
                elapsed += Time.deltaTime;
                yield return null;
            }
            _fillImage.fillAmount = to;
        }
        
    }
}