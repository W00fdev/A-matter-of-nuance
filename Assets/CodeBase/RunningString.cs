using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunningString : MonoBehaviour
{
    TextMeshProUGUI text;
    string intro_story = "ј вам приходилось управл€ть королевством?";
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(IntroStoryBySymbols());
    }
    IEnumerator IntroStoryBySymbols()
    {
        for (int i = 0;i < intro_story.Length;i++)
        {
            text.text += intro_story[i];
            yield return new WaitForSeconds(0.5f);
        }
    }
 }
