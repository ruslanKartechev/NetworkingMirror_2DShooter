using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{ 
    public class ImageFlicker : MonoBehaviour
    {
        public float duration = 0.1f;
        public int count = 3;
        [SerializeField] private SpriteRenderer _image;
        private Coroutine _flicking;

        public void Flick()
        {
             StopFlicking();
             _flicking = StartCoroutine(Flicking());
        }

        public void StopFlicking()
        {  
            if(_flicking != null)
                StopCoroutine(_flicking);
        }
        private IEnumerator Flicking()
        {
            var flicks = 0;
            while (flicks < count)
            {
                _image.enabled = false;
                yield return new WaitForSeconds(duration);
                _image.enabled = true;
                yield return new WaitForSeconds(duration);
                flicks++;
            }
        }
    }
}