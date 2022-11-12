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
        public float cooldown;
        public bool hasOnlyOneLife;

        [Header("References")]
        public Transform actorSpotsContainer;
        public Transform cloud;
        public GameManager manager;

        public UnityEvent onKingDied;

        public static bool isFreezed;

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

        private void Start()
        {
            GetKing().DiedEvent += OnKingDied;
        }

        private void Update()
        {
            Unit unit = GetTraitor();
            if (unit == null)
                return;

            unit.NO(isFreezed);
        }
        private void OnKingDied()
        {
            if (hasOnlyOneLife)
                manager.OnLose();
            else
            {
                if (GetTraitor() == null)
                    manager.OnLose();
                else
                    Destroy(GetTraitor().gameObject);
            }

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

                if (UnityEngine.Random.value > Constants.BetrayChance)
                    continue;

                if (!Constants.IsGameStarted)
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
