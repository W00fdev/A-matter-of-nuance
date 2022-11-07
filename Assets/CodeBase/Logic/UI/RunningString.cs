using UnityEngine.Events;
using System.Collections;
using Infrastructure;
using UnityEngine;
using TMPro;

namespace Logic.UI
{
    public class RunningString : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        [Multiline, SerializeField]
        private string _introStory;

        [SerializeField]
        private float _speed = 2f;

        private Coroutine _coroutine = null;
        private bool _disabled;

        public UnityEvent ClickAfterFullShown;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _coroutine = StartCoroutine(IntroStoryBySymbols());
        }

        private void Update()
        {
            if (InputService.Instance.IsLeftMouseButtonDown() && _disabled == false)
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                    _coroutine = null;

                    _text.text = _introStory;
                }
                else
                {
                    ClickAfterFullShown?.Invoke();
                    DisableObject();
                }
            }
        }

        private void DisableObject()
        {
            _disabled = true;
            _text.text = _introStory;
            Destroy(gameObject, Constants.IntroTime);
        }

        IEnumerator IntroStoryBySymbols()
        {
            WaitForSeconds introSecondsTick = new WaitForSeconds(1f / _speed);
            for (int i = 0; i < _introStory.Length; i++)
            {
                _text.text += _introStory[i];
                yield return introSecondsTick;
            }

            _coroutine = null;
        }
    }

}
