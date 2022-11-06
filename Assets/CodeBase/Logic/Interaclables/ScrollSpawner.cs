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

        private RandomizedCycle<ScrollData> _dataCycle;

        private void Start()
        {
            _dataCycle = new(Resources.LoadAll<ScrollData>("Scrolls"));
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
                if (_scrollDrawer != null)
                    _scrollDrawer.Reveal(_dataCycle.GetNext());
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