using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ImageFader : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration;
        [SerializeField] private List<ImageData> _images;
        private Coroutine _fading;

        public void FadeIn()
        {
            StopFading();
            _fading = StartCoroutine(FadingIn());
        }

        public void FadeOut()
        {
            StopFading();
            _fading = StartCoroutine(FadingOut());
        }

        public void Show()
        {
            foreach (var im in _images)
                im.image.enabled = true;
        }

        public void Hide()
        {
            foreach (var im in _images)
                im.image.enabled = false;
        }

        private void StopFading()
        {
            if (_fading != null)
                StopCoroutine(_fading);
        }

        private IEnumerator FadingIn()
        {
            Show();
            var elapsed = 0f;
            var time = _fadeDuration;
            while (elapsed < time)
            {
                foreach (var im in _images)
                    im.SetAlphaStartEnd(elapsed / time);
                elapsed += Time.deltaTime;
                yield return null;
            }

            foreach (var im in _images)
                im.SetAlphaStartEnd(1);
        }

        private IEnumerator FadingOut()
        {
            Show();
            var elapsed = 0f;
            var time = _fadeDuration;
            while (elapsed < time)
            {
                foreach (var im in _images)
                    im.SetAlphaEndStart(elapsed / time);
                elapsed += Time.deltaTime;
                yield return null;
            }

            foreach (var im in _images)
                im.image.enabled = false;
        }
        
        [System.Serializable]
        public class ImageData
        {
            public float startA;
            public float endA;
            public Image image;

            public void SetAlphaStartEnd(float t)
            {
                var color = image.color;
                color.a = Mathf.Lerp(startA, endA, t);
                image.color = color;
            }
            
            public void SetAlphaEndStart(float t)
            {
                var color = image.color;
                color.a = Mathf.Lerp(endA, startA, t);
                image.color = color;
            }
        }
    }
}