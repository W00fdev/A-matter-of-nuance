using Infrastructure;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Logic.Actors
{
    public class TraitorManager : MonoBehaviour, IManager
    {
        [Header("Settings")]
        public float periodInSeconds;
        [Range(0f, 1f)]
        public float chance;
        public float cooldown;

        [Header("References")]
        public Transform actorSpotsContainer;
        public Transform cloud;
        public GameManager manager;

        public UnityEvent onKingDied;

        public bool isFreezed;

        public Unit GetKing() => actorSpotsContainer.GetChild(0).GetComponentInChildren<Unit>();
        public Unit GetTraitor() => actorSpotsContainer.GetChild(1).GetComponentInChildren<Unit>();

        public void EnableManager(bool instant)
        {
            if (instant == false)
                StartCoroutine(WaitAndDo(Constants.IntroTime, StartWrapper));
            else
                StartWrapper();
        }

        public void DisableManager() { }

        private void Start() => GetKing().DiedEvent += OnKingDied;

        private void OnKingDied()
        {
            if (GetTraitor() == null)
                manager.OnLose();
            else
                Destroy(GetTraitor().gameObject);

            foreach (Transform spot in actorSpotsContainer)
            {
                if (spot.name == "0")
                    continue;

                var unit = spot.GetComponentInChildren<Unit>();
                
                if (unit != null)
                    unit.Run();
            }
        }

        private void StartWrapper() => StartCoroutine(WaitAndDo(cooldown, () => StartCoroutine(PeriodicBetrayal())));

        IEnumerator WaitAndDo(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }

        IEnumerator PeriodicBetrayal()
        {
            while (true)
            {
                yield return new WaitForSeconds(periodInSeconds);

                if (isFreezed)
                    continue;

                if (UnityEngine.Random.value > chance)
                    continue;

                var traitor = GetTraitor();

                if (traitor == null)
                    continue;

                traitor.Kill(GetKing(), cloud);
                break;
            }

            StartWrapper();
        }
    }
}
