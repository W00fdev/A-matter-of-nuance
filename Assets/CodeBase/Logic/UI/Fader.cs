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

        // By unityEvent (RunningString ->)
        public void ToMainScene()
        {
            Bootstrapper.StartGame(instant: false);
            StartCoroutine(Fade());
        }

        IEnumerator Fade()
        {
            yield return new WaitForSeconds(0.5f);

            var faderTick = new WaitForSeconds(1f / 20f);
            for (float f = 1; f >= 0; f -= 1f / 20f)
            {
                yield return faderTick;
                _canvasGroup.alpha = f;
            }
            _canvasGroup.alpha = 0;

            soundEnable.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
