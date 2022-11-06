using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Data;
using System;

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

        public event Action<Variant> AcceptEvent;
        public event Action<Variant> DeclineEvent;

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


            // Disable player movement
            Constants.AllowedMovement = false;
        }

        public void Accept()
        {
            afterSelectText.text = _lastData.variants[0].consequence.text;
            AcceptEvent?.Invoke(_lastData.variants[0]);
        }

        public void Decline()
        {
            afterSelectText.text = _lastData.variants[1].consequence.text;
            DeclineEvent?.Invoke(_lastData.variants[1]);
        }

        public void Close()
        {
            Constants.AllowedMovement = true;
            gameObject.SetActive(false);
        }
    }
}