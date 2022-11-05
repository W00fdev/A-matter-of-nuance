using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIComments : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject gameObject;
    public void Start()
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
