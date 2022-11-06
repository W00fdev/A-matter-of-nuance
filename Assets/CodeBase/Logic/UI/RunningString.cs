using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Logic.UI
{
    public class RunningString : MonoBehaviour
    {
        TextMeshProUGUI text;
        [Multiline, SerializeField]
        private string _introStory;

        [SerializeField]
        private float _speed = 2f;

        public UnityEvent onEnd;

        private void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            StartCoroutine(IntroStoryBySymbols());
        }

        IEnumerator IntroStoryBySymbols()
        {
            for (int i = 0; i < _introStory.Length; i++)
            {
                text.text += _introStory[i];
                yield return new WaitForSeconds(1f / _speed);
            }

            onEnd.Invoke();
        }


    }

}
