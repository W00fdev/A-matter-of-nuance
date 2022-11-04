using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class YesNoOutput : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject gameObject;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        gameObject.SetActive(true);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        gameObject.SetActive(false);
    }
}
