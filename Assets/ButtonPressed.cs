using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPressed : MonoBehaviour
{
    public GameObject green;
    public GameObject red;
    public Animator animator1;
    public Animator animator2;
    public Button button;
    TextMeshProUGUI text;
    public void Start()
    {
        green.SetActive(false);
        red.SetActive(false);
    }
    public void Pressed()
    {
        GetComponent<Image>().enabled = false;
        green.SetActive(true);
        red.SetActive(true);
        StartCoroutine(TextFade());
    }
    IEnumerator TextFade()
    {

        for(float i = 1f; i > 0f; i-=0.05f)
        {
            text.alpha-= i;
        }
        yield return new WaitForSeconds(0.05f);
    }
}
