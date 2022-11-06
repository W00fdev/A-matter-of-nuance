using UnityEngine;

namespace Logic.Interactables
{
    public class Waypointer : MonoBehaviour
    {
        public bool HideMe = false;

        void Start()
        {
            if (HideMe == true)
                GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}