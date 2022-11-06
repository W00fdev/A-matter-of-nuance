using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Actors
{
    public class VassalSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _prefabs;

        public Transform actorsContainer;
        public Transform actorSpotsContainer;
    }
}