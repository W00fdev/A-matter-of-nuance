using Logic.Interactables;
using UnityEngine;
using System;
using Data;

namespace Logic.Actors
{
    public class VassalSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _prefabs;

        public Transform actorSpotsContainer;
        public ScrollSpawner scrollSpawner;

        public event Action VassalSpawnedEvent;

        private void Start()
        {
            scrollSpawner._scrollDrawer.AcceptEvent += OnMakeDecision;
            scrollSpawner._scrollDrawer.DeclineEvent += OnMakeDecision;
        }

        private void OnMakeDecision(Variant variant)
        {
            if (variant.consequence.hasToCreateVassal)
                Spawn();
        }

        public void Spawn()
        {
            Transform nextSpot = null;

            foreach (Transform spot in actorSpotsContainer)
                if (spot.childCount == 0)
                {
                    nextSpot = spot;
                    break;
                }

            if (nextSpot == null)
                return;

            Instantiate(_prefabs[UnityEngine.Random.Range(0, _prefabs.Length)], nextSpot);
            VassalSpawnedEvent?.Invoke();
        }
    }
}