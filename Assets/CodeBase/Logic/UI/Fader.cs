using System.Collections;
using Infrastructure;
using UnityEngine;

namespace Logic.UI
{
    public class Fader : MonoBehaviour
    {
        public Bootstrapper Bootstrapper;
        private CanvasGroup _canvasGroup;

        public GameObject soundEnable;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ToMainScene()
        {
            Bootstrapper.StartGame(instant: false);
            StartCoroutine(Fade());
        }

        IEnumerator Fade()
        {
            for (float f = 1; f >= 0; f -= 1f / 20f)
            {
                _canvasGroup.alpha = f;
                yield return new WaitForSeconds(3f / 20f);
            }
            _canvasGroup.alpha = 0;

            soundEnable.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
