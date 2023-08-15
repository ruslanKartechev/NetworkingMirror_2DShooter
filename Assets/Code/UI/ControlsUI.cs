using System.Collections;
using GameCore.Player;
using UnityEngine;

namespace UI
{
    public class ControlsUI : MonoBehaviour, IControlsUI
    {
        [SerializeField] private float _maxRad = 150f;
        [SerializeField] private float _sensitivity = 100f;
        [SerializeField] private RectTransform _movable;
        [SerializeField] private ImageFader _fader;
        [SerializeField] private Transform _block;
        private IMoveInputListener _moveInputListener;
        private IFireInputListener _fireInputListener;
        private Coroutine _inputChecking;

        private float Distance2 => _movable.anchoredPosition.sqrMagnitude;
        private float Rad2 => _maxRad * _maxRad;
        private float Magn => Distance2 / Rad2;
        
        private void Awake()
        {
            _fader.Hide();
        }
        
        public void SetMoveInputListener(IMoveInputListener listener) => _moveInputListener = listener;
        public void SetFireListener(IFireInputListener fireInputListener) => _fireInputListener = fireInputListener;

        public void TurnOn()
        {
            StartInputCheck();      
        }

        public void TurnOff()
        {
            StopInputCheck();
            _movable.anchoredPosition = Vector2.zero;
            _fader.FadeOut();
        }

        private void StopInputCheck()
        {
            if(_inputChecking != null)
                StopCoroutine(_inputChecking);
        }

        private void StartInputCheck()
        {
            StopInputCheck();
            _inputChecking = StartCoroutine(InputCheck());
        }

        private IEnumerator InputCheck()
        {
            var prevPos =  Input.mousePosition;
            var currentPos = prevPos;
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _fader.FadeIn();
                    prevPos = currentPos = Input.mousePosition;
                    _movable.anchoredPosition = Vector2.zero;
                    _block.position = currentPos;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    _movable.anchoredPosition = Vector2.zero;
                    _fader.FadeOut();
                }
                else if (Input.GetMouseButton(0))
                {
                    currentPos = Input.mousePosition;
                    var newPos = _movable.anchoredPosition + ((Vector2)(currentPos - prevPos).normalized) * (_sensitivity * Time.deltaTime);
                    if (newPos.sqrMagnitude > Rad2)
                        newPos = newPos.normalized * _maxRad;
                    _movable.anchoredPosition = newPos;
                    _moveInputListener.Move(_movable.anchoredPosition.normalized, Magn);
                    prevPos = currentPos;
                    _fireInputListener.Fire();
                }
                yield return null;
            }
        }
        
        
    }
}