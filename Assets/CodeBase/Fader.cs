using Infrastructure;
using Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public StartScreen startScreen;
    CanvasGroup canvasGroup;
    public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    IEnumerator Fade()
    {
        for (float f = Constants.IntroTime/Constants.IntroTime; f >= 0; f -= 1f/20f)
        {
            canvasGroup.alpha = f;
            yield return new WaitForSeconds(1f/20f * Constants.IntroTime);
        }
        canvasGroup.alpha = 0;
        startScreen.StartGame();
    }
    public void ToMainScene()
    {
        StartCoroutine(Fade());
    }
}
