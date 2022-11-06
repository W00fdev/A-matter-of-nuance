using System.Collections.Generic;
using Utilities.Physics;
using UnityEngine;
using Logic.UI;
using Logic.Actors;
using Data;

namespace Logic.Interactables
{
    public class ScrollSpawner : MonoBehaviour
    {
        public ScrollDrawer _scrollDrawer;
        public ResourcesManager ResourcesManager;

        private ScrollData[] _scrollsData;
        private Queue<ScrollData> _scrollQueue = new();

        private void Start()
        {
            _scrollsData = Resources.LoadAll<ScrollData>("Scrolls");
            _scrollDrawer.AcceptEvent += OnMakeDecision;
            _scrollDrawer.DeclineEvent += OnMakeDecision;
        }

        // listener for RoomsSpawner.onRoomSpawned
        public void HandleNewRoom(RoomRunner room)
        {
            var adapter = room.scrollContainer.GetComponentInChildren<TriggerEventAdapter>();
            adapter.onEnter.AddListener(Spawn);
        }

        // calls when unit and scroll collides
        private void Spawn(GameObject gameObject)
        {
            if (gameObject.TryGetComponent<Unit>(out _))
                RevealNext();
        }

        private void RevealNext()
        {
            if (_scrollQueue.Count == 0)
                Requeue();

            var data = _scrollQueue.Dequeue();

            if (_scrollDrawer != null)
                _scrollDrawer.Reveal(data);
        }

        private void Requeue()
        {
            List<int> used = new();
            _scrollQueue = new Queue<ScrollData>(_scrollsData.Length);

            while (_scrollQueue.Count != _scrollsData.Length)
            {
                int nextId = Random.Range(0, _scrollsData.Length);

                if (used.Contains(nextId))
                    continue;

                _scrollQueue.Enqueue(_scrollsData[nextId]);
                used.Add(nextId);
            }
        }
    
        private void OnMakeDecision(Variant variant)
        {
            foreach (var item in variant.values)
                ApplyDeltaValues(item);
        }

        private void ApplyDeltaValues(Value value)
        {
            switch (value.resourceType)
            {
                case ResourceWinType.RELIGION:
                    ResourcesManager.ChangeReligion(value.deltaValue);
                        break;
                case ResourceWinType.FOOD:
                    ResourcesManager.ChangeFood(value.deltaValue);
                    break;
                case ResourceWinType.ARMY:
                    ResourcesManager.ChangeArmy(value.deltaValue);
                    break;
            }
        }
    }
}