using Data;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Physics;
using Logic.UI;

namespace Logic
{
    public class ScrollSpawner : MonoBehaviour
    {
        public ScrollDrawer _scrollDrawer;

        private ScrollData[] _scrollsData;
        private Queue<ScrollData> _scrollQueue = new();

        private void Start() => _scrollsData = Resources.LoadAll<ScrollData>("Scrolls");

        public void HandleNewRoom(RoomRunner room)
        {
            var adapter = room.scrollContainer.GetComponentInChildren<TriggerEventAdapter>();
            adapter.onEnter.AddListener(Spawn);
        }

        private void Spawn(GameObject gameObject)
        {
            Unit unit = gameObject.GetComponent<Unit>();

            if (unit == null)
                return;

            RevealNext();
        }

        private void RevealNext()
        {
            if (_scrollQueue.Count == 0)
                Requeue();

            var scroll = _scrollQueue.Dequeue();

            if (_scrollDrawer != null)
                _scrollDrawer.Reveal(scroll);
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
    }
}