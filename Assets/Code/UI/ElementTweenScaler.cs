using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class ElementTweenScaler : MonoBehaviour
    {
        [SerializeField] private Transform _scalable;
        [SerializeField] private Vector3 _scale1;
        [SerializeField] private Vector3 _scale2;
        [SerializeField] private float _scaleTime1;
        [SerializeField] private float _scaleTime2;
        private Sequence _sequence;
        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (_scalable == null)
                _scalable = transform;
        }
#endif
        private void Start()
        {
            Begin();
            
        }

        public void Begin()
        {
            _sequence = DOTween.Sequence();
            _scalable.localScale = _scale1;
            _sequence.Append(_scalable.DOScale(_scale2, _scaleTime2));
            _sequence.Append(_scalable.DOScale(_scale1, _scaleTime1));
            _sequence.SetLoops(-1);
        }

        public void Stop()
        {
            _sequence?.Kill();
        }
    }
}