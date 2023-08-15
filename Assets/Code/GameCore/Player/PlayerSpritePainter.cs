using UnityEngine;
using UnityEngine.UI;

namespace GameCore.Player
{
    [System.Serializable]
    public class PlayerSpritePainter
    {
        [SerializeField] private SpriteRenderer _front;
        [SerializeField] private SpriteRenderer _back;
        
        // null checks are temporary
        public void SetColor(Color color)
        {
            if(_front != null)
                _front.color = color;
            if (_back == null)
                return;
            var backColor = color * 0.38f;
            backColor.a = 1f;
            _back.color = backColor;   
        }
    }
    
    [System.Serializable]
    public class PlayerSpritePainterUI
    {
        [SerializeField] private Image _front;
        [SerializeField] private Image _back;
        public void SetColor(Color color)
        {
            _front.color = color;
            var backColor = color * 0.38f;
            backColor.a = 1f;
            _back.color = backColor;   
        }
    }

}