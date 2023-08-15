using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CoinsUI : MonoBehaviour, ICoinsUI
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Transform _scalable;
        [SerializeField] private float _scalePunch;
        private int _count;
        
        public void SetCount(float count)
        {
            _count = ((int)count);
            _text.text = _count.ToString();
        }

        public void AddCount(float count)
        {
            _count += (int)count;
            _text.text = _count.ToString();
            _scalable.localScale = Vector3.one;
            _scalable.DOKill();
            _scalable.DOPunchScale(Vector3.one * _scalePunch, 0.25f);
        }

        public void UpdateCoins(float count)
        {
            _count = (int)count;
            _text.text = _count.ToString();
            _scalable.localScale = Vector3.one;
            _scalable.DOKill();
            _scalable.DOPunchScale(Vector3.one * _scalePunch, 0.25f);
        }
    }
}