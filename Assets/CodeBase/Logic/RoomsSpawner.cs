using System.Collections.Generic;
using System.Collections;
using Infrastructure;
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

        public void EnableManager(bool instant)
        {
            if (instant == false)
                StartCoroutine(FaderBeforeManager());
            else
                StartWrapper();
        }

        public void DisableManager()
        {
        }

        private void BuildFirst()
            => BuildNewRoom(0f, first: true);

        private void BuildNext(float offsetJoint2D)
            => BuildNewRoom(offsetJoint2D);

        private void BuildNewRoom(float offsetJoint2D, bool first = false)
        {
            RoomData nextRoom = GetNextRoom();
            RoomRunner scriptRoom = BuildRoom(nextRoom, (!first ? CameraBoundsX.y : CameraBoundsX.x) - offsetJoint2D);

            if (Random.value >= trapBuildChance)
                BuildRandomTrap(scriptRoom);

            InitializeNewRoom(nextRoom, scriptRoom, offsetJoint2D, firstRoom: first);
        }

        private void InitializeNewRoom(RoomData nextRoom, RoomRunner scriptRoom, float offsetJoint2D, bool firstRoom = false)
        {
            scriptRoom.OutOfBoundsEvent += BuildNext;
            scriptRoom.FirstOffset = (firstRoom == false) ? offsetJoint2D : Mathf.Abs(CameraBoundsX.x) + Mathf.Abs(CameraBoundsX.y);
            scriptRoom.CameraBoundsX = Mathf.Abs(CameraBoundsX.x) + Mathf.Abs(CameraBoundsX.y);
            scriptRoom.Length = nextRoom.Length;
            scriptRoom.Enable();
        }

        private void BuildRandomTrap(RoomRunner room)
        {
            if (trapPrefabs.Count == 0)
                return;

            Instantiate(trapPrefabs[Random.Range(0, trapPrefabs.Count)],
                        room.trapSpotsContainer.GetChild(Random.Range(0, room.trapSpotsContainer.childCount)));
        }

        private RoomData GetNextRoom() => Rooms[0];

        private RoomRunner BuildRoom(RoomData room, float positionX) =>
            Instantiate(room.RoomPrefab, new Vector3(positionX, 0f, 0f), Quaternion.identity, RoomsParent).GetComponent<RoomRunner>();
    


        IEnumerator FaderBeforeManager()
        {
            yield return new WaitForSeconds(Constants.IntroTime);
            
            StartWrapper();
        }

        private void StartWrapper() 
            => BuildFirst();
    }
}
