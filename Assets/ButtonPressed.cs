using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPressed : MonoBehaviour
{
    public GameObject green;
    public GameObject red;
    public GameObject yes;
    public GameObject no;
    public GameObject comment;
    public Animator animator1;
    public Animator animator2;
    public Button button;
    public TextMeshProUGUI text;
    public void Start()
    {
        red.SetActive(false);
        green.SetActive(false);
    }
    public void YesPressed()
    {
        comment.SetActive(false);
        GetComponent<Image>().enabled = false;
        GetComponent<UIComments>().enabled = false;
        green.SetActive(true);
        no.SetActive(false);
        StartCoroutine(TextFade());
    }
    public void NoPressed()
    {
        comment.SetActive(false);
        GetComponent<Image>().enabled = false;
        GetComponent<UIComments>().enabled = false;
        red.SetActive(true);
        yes.SetActive(false);
        StartCoroutine(TextFade());
    }
    IEnumerator TextFade()
    {

        for(float i = 1f; i >= 0f; i-=0.05f)
        {
            text.color = new Color(text.color.r,text.color.g,text.color.b,i);
            yield return new WaitForSeconds(0.05f);
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
    }
}
