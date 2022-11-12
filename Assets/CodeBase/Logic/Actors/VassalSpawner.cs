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
        public event Action VassalDeletedEvent;

        public int GetCount()
        {
            int count = 0;

            foreach (Transform spot in actorSpotsContainer)
            {
                if (spot.childCount == 0)
                    break;

                count++;
            }

            return count - 1;
        }

        private RandomizedCycle<GameObject> _prefabCycle;

        private void Start()
        {
            _prefabCycle = new(_prefabs);
            scrollSpawner._scrollDrawer.AcceptEvent += OnMakeDecision;
            scrollSpawner._scrollDrawer.DeclineEvent += OnMakeDecision;
        }

        private void OnMakeDecision(Variant variant)
        {
            if (variant.consequence.hasToCreateVassal)
                Spawn();

            if (variant.consequence.hasToLoseVassal)
                Kick();
        }

        public void Sort() { }

        public void Kick()
        {
            Transform last = null;

            foreach (Transform spot in actorSpotsContainer)
            {
                if (spot.childCount == 0)
                {
                    if (last != null)
                    {
                        Unit unit = last.GetChild(0).GetComponent<Unit>();

                        if (unit.isKing)
                            break;

                        unit.Run();

                        VassalDeletedEvent?.Invoke();
                        break;
                    }
                }

                last = spot;
            }
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

            Instantiate(_prefabCycle.GetNext(), nextSpot);
            VassalSpawnedEvent?.Invoke();
        }
    }
}