using UnityEngine;
using UnityEngine.EventSystems;

namespace Utilities.UI
{
    public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject tip;

        public void Start() => tip.SetActive(false);

        public void OnPointerEnter(PointerEventData pointerEventData) => tip.SetActive(true);

        public void OnPointerExit(PointerEventData pointerEventData) => tip.SetActive(false);
    }
}