using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI
{
    public class ScrollDrawer : MonoBehaviour
    {
        public TMP_Text descriptionText;
        public TMP_Text afterSelectText;
        public Button[] scrollButtons;

        public void Reveal(ScrollData data)
        {
            Debug.Log(data.description);

            descriptionText.text = data.description;

            for (int i = 0; i < data.variants.Length; i++)
            {
                scrollButtons[i].GetComponentInChildren<TMP_Text>().text = data.variants[i].tooltip;
                scrollButtons[i].onClick.AddListener(delegate { AcceptConsequences(data.variants[i].consequence); });
            }
        }

        private void AcceptConsequences(Consequence consequence)
        {
            Debug.Log(consequence.id);
            afterSelectText.text = consequence.text;
        }
    }
}