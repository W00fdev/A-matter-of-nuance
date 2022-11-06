using System.Collections.Generic;
using System.Collections;
using Infrastructure;
using UnityEngine;
using UnityEngine.Events;

namespace Logic
{
    public class RoomsSpawner : MonoBehaviour, IManager
    {
        public Transform RoomsParent;

        [Header("Set X's of camera left and right bounds")]
        public Vector2 CameraBoundsX;

        public List<GameObject> RoomPrefabs;

        public UnityEvent<RoomRunner> onRoomSpawned;

        public float DecorBuildChance = 0.43f;

        public RoomRunner LastRoom { get; private set; }

        public void EnableManager(bool instant)
        {
            if (instant == false)
                StartCoroutine(FaderBeforeManager());
            else
                StartWrapper();
        }

        public void DisableManager() { }

        private void BuildFirst()
            => BuildNewRoom(0f, first: true);

        private void BuildNext(float offsetJoint2D)
            => BuildNewRoom(offsetJoint2D);

        private void BuildNewRoom(float offsetJoint2D, bool first = false)
        {
            RoomRunner roomPrefab = GetNextRoom();
            RoomRunner room = BuildRoom(roomPrefab, (!first ? CameraBoundsX.y : CameraBoundsX.x) - offsetJoint2D);
            
            InitializeTraps(room);
            InitializeDecor(room);
            InitializeNewRoom(room, offsetJoint2D, firstRoom: first);
        }

        private void InitializeTraps(RoomRunner scriptRoom)
        {
            if (Random.value <= Constants.TrapChance)
                BuildRandomTrap(scriptRoom);
        }

        private void InitializeDecor(RoomRunner scriptRoom)
        {
            for (int i = 0; i < scriptRoom.decorSpotsContainer.childCount; i++)
                if (Random.value >= DecorBuildChance)
                    BuildRandomDecor(scriptRoom, waypointIndex: i);
        }

        private void InitializeNewRoom(RoomRunner scriptRoom, float offsetJoint2D, bool firstRoom = false)
        {
            scriptRoom.OutOfBoundsEvent += BuildNext;
            scriptRoom.FirstOffset = (firstRoom == false) ? offsetJoint2D : Mathf.Abs(CameraBoundsX.x) + Mathf.Abs(CameraBoundsX.y);
            scriptRoom.CameraBoundsX = Mathf.Abs(CameraBoundsX.x) + Mathf.Abs(CameraBoundsX.y);
            scriptRoom.Enable();
            LastRoom = scriptRoom;
            onRoomSpawned.Invoke(scriptRoom);
        }

        private void BuildRandomTrap(RoomRunner room)
        {
            int waypointIndex = Random.Range(0, room.trapSpotsContainer.childCount);
            BuildRandomTrapOrDecor(room.TrapPrefabsContainer, room.trapSpotsContainer, waypointIndex);
        }

        private void BuildRandomDecor(RoomRunner room, int waypointIndex) 
            => BuildRandomTrapOrDecor(room.DecorPrefabsContainer, room.decorSpotsContainer, waypointIndex);

        private void BuildRandomTrapOrDecor(List<GameObject> prefabs, Transform spotParent, int waypointIndex)
        {
            if (prefabs == null)
                return; 

            int count = prefabs.Count;
            if (count == 0)
                return;

            Instantiate(prefabs[Random.Range(0, count)], spotParent.GetChild(waypointIndex));
        }

        private RoomRunner GetNextRoom() => RoomPrefabs[Random.Range(0, RoomPrefabs.Count)].GetComponent<RoomRunner>();

        private RoomRunner BuildRoom(RoomRunner prefab, float positionX) =>
            Instantiate(prefab, new Vector3(positionX, 0f, 0f), Quaternion.identity, RoomsParent).GetComponent<RoomRunner>();
    
        IEnumerator FaderBeforeManager()
        {
            yield return new WaitForSeconds(Constants.IntroTime);
            
            StartWrapper();
        }

        private void StartWrapper() 
            => BuildFirst();
    }
}
