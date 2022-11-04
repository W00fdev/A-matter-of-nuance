using Logic;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    public class StartScreen : MonoBehaviour
    {
        public List<MonoBehaviour> AllManagers;

        private void Awake()
        {

        }

        private void StartGame()
        {
            foreach (IManager manager in AllManagers)
                manager.EnableManager();
        }

        private void Update()
        {

        }
    }
}


