using Logic.Interactables;
using UnityEngine;
using System;
using Data;
using System.Collections;
using Mono.Cecil;

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

            Constants.VassalsCount = count - 1;
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
            // child(0) is king/
            for (int i = actorSpotsContainer.childCount - 1; i > 0; i--)
            {
                Transform spot = actorSpotsContainer.GetChild(i);
                if (spot.childCount != 0)
                {
                    if (spot.GetChild(0).TryGetComponent(out Unit unit))
                    {
                        unit.Run();
                        Constants.VassalsCount--;

                        VassalDeletedEvent?.Invoke();
                        return;
                    }
                }
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

            SpriteRenderer vassalSprite = Instantiate(_prefabCycle.GetNext(), nextSpot).transform.GetComponent<SpriteRenderer>();

            // Remove the bug with sprite jiggling on instantiate
            vassalSprite.enabled = false;
            StartCoroutine(ShowVassalNextFrame(vassalSprite));
            // Remove the bug with sprite jiggling on instantiate

            Constants.VassalsCount++;
            VassalSpawnedEvent?.Invoke();
        }

        IEnumerator ShowVassalNextFrame(SpriteRenderer spriteRenderer)
        {
            yield return null;
            spriteRenderer.enabled = true;
        }
    }
}