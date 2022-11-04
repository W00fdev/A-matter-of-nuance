using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    IEnumerator Fade()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            canvasGroup.alpha = f;
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void ToMainScene()
    {
        StartCoroutine("Fade");
    }
}
