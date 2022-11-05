using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        public List<MonoBehaviour> AllManagers;

        public bool InstantStart;

        private void Awake()
        {
            if (InstantStart)
                StartGame(instant: true);
        }

        public void StartGame(bool instant)
        {
            foreach (IManager manager in AllManagers.Cast<IManager>())
                manager.EnableManager(instant);
        }
    }
}


