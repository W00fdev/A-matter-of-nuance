using System.Collections;
using UnityEngine;
using TMPro;

namespace Logic.UI
{
    public class RunningString : MonoBehaviour
    {
        TextMeshProUGUI text;
        [Multiline, SerializeField]
        private string _introStory;

        [SerializeField]
        private float _speed = 2f;

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
        }
    }

}
