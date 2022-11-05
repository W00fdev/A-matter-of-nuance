using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Data;

namespace Logic.UI
{
    public class ScrollDrawer : MonoBehaviour
    {
        public TMP_Text descriptionText;
        public TMP_Text afterSelectText;

        public Transform accept;
        public TMP_Text acceptText;
        public Transform refuse;
        public TMP_Text refuseText;

        private ScrollData _lastData;

        public void Reveal(ScrollData data)
        {
            gameObject.SetActive(true);
            descriptionText.text = data.description;

            if (data.variants.Length == 0)
                throw new UnityException("no variants in scroll data");

            accept.gameObject.SetActive(true);
            refuse.gameObject.SetActive(data.variants.Length > 1);

            acceptText.text = data.variants[0].tooltip;

            if (data.variants.Length > 1)
                refuseText.text = data.variants[1].tooltip;

            _lastData = data;
        }

        public void Accept() => afterSelectText.text = _lastData.variants[0].consequence.text;

        public void Decline() => afterSelectText.text = _lastData.variants[1].consequence.text;

        public void Close() => gameObject.SetActive(false);
    }
}