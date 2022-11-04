using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Data;

namespace Logic
{
    public class RoomsSpawner : MonoBehaviour, IManager
    {
        public Transform RoomsParent;

        [Header("Set X's of camera left and right bounds")]
        public Vector2 CameraBoundsX;

        public List<RoomData> Rooms;
        public List<GameObject> trapPrefabs;

        [Range(0f, 1f)]
        public float trapBuildChance;

        public void EnableManager()
        {
            StartCoroutine(FaderBeforeManager());
        }

        public void DisableManager()
        {

        }

        private void BuildFirst()
            => BuildNewRoom(first: true);

        private void BuildNext()
            => BuildNewRoom();

        private void BuildNewRoom(bool first = false)
        {
            RoomData nextRoom = GetNextRoom();
            RoomRunner scriptRoom = BuildRoom(nextRoom, !first ? CameraBoundsX.y : CameraBoundsX.x);
<<<<<<< Updated upstream
=======

            if (Random.value >= trapBuildChance)
                BuildRandomTrap(scriptRoom);
>>>>>>> Stashed changes

            InitializeNewRoom(nextRoom, scriptRoom, firstRoom: first);
        }

        private void InitializeNewRoom(RoomData nextRoom, RoomRunner scriptRoom, bool firstRoom = false)
        {
            scriptRoom.OutOfBoundsEvent += BuildNext;
            scriptRoom.FirstOffset = (firstRoom == false) ? 0f : Mathf.Abs(CameraBoundsX.x) + Mathf.Abs(CameraBoundsX.y);
            scriptRoom.CameraBoundsX = Mathf.Abs(CameraBoundsX.x) + Mathf.Abs(CameraBoundsX.y);
            scriptRoom.Length = nextRoom.Length;
            scriptRoom.Enable();
        }

        private RoomData GetNextRoom() => Rooms[0];

        private RoomRunner BuildRoom(RoomData room, float positionX) =>
            Instantiate(room.RoomPrefab, new Vector3(positionX, 0f, 0f), Quaternion.identity, RoomsParent).GetComponent<RoomRunner>();
<<<<<<< Updated upstream
    
        IEnumerator FaderBeforeManager()
        {
            yield return new WaitForSeconds(Constants.IntroTime);
            BuildFirst();
        }
    }

    public interface IManager
    {
        public void EnableManager();

        public void DisableManager();
=======

        private void BuildRandomTrap(RoomRunner room)
        {
            if (trapPrefabs.Count == 0)
                return;

            Instantiate(trapPrefabs[Random.Range(0, trapPrefabs.Count)],
                        room.trapSpotsContainer.GetChild(Random.Range(0, room.trapSpotsContainer.childCount)));
        }
>>>>>>> Stashed changes
    }
}
