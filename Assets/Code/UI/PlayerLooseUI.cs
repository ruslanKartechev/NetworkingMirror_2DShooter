using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerLooseUI : MonoBehaviour, IPlayerLooseUI
    {
        [SerializeField] private TextMeshProUGUI _playerNameText;
        [SerializeField] private float _scaleTime;
        [SerializeField] private Vector3 _fromScale;
        [SerializeField] private Transform _scalable;
        
        public void Show(string playerName)
        {
            _playerNameText.text = playerName;
            _scalable.localScale = _fromScale;
            _scalable.DOScale(Vector3.one, _scaleTime).SetEase(Ease.InQuad);
        }
    }
}