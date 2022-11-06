using System.Collections;
using Infrastructure;
using UnityEngine;

namespace Logic.UI
{
    public class Fader : MonoBehaviour
    {
        public Bootstrapper Bootstrapper;
        private CanvasGroup _canvasGroup;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ToMainScene()
        {
            StartCoroutine(Fade());
        }

        IEnumerator Fade()
        {
            for (float f = Constants.IntroTime / Constants.IntroTime; f >= 0; f -= 1f / 20f)
            {
                _canvasGroup.alpha = f;
                yield return new WaitForSeconds(1f / 20f * Constants.IntroTime);
            }
            _canvasGroup.alpha = 0;
            Bootstrapper.StartGame(instant: false);
        }
    }
}
