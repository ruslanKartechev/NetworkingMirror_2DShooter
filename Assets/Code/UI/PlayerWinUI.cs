using DG.Tweening;
using Mirror;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerWinUI : MonoBehaviour, IPlayerWinUI
    {
        [SerializeField] private TextMeshProUGUI _playerNameText;
        [SerializeField] private TextMeshProUGUI _coinsCount;
        [Space(10)] 
        [SerializeField] private float _scaleTime;
        [SerializeField] private Vector3 _fromScale;
        [SerializeField] private Transform _scalable;
        
        public void Show(string playerName, int coinsCount)
        {
            _scalable.localScale = _fromScale;
            _scalable.DOScale(Vector3.one, _scaleTime).SetEase(Ease.InQuad);
            _coinsCount.text = $"{coinsCount}";
            _playerNameText.text = playerName;
        }
    }
}