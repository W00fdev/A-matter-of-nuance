using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.ThirdParty.Plugins
{
    public class YandexSDK : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void Hello();

        [DllImport("__Internal")]
        private static extern void RateGame();

        private void Start()
        {
            Hello();
        }

        public void RateGameJS()
            => RateGame();
    }
}
