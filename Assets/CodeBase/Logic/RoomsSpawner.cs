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

        public List<GameObject> RoomPrefabs;
        public List<GameObject> trapPrefabs;

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
            
            InitializeTraps(scriptRoom);
            InitializeDecor(scriptRoom);
            InitializeNewRoom(nextRoom, scriptRoom, offsetJoint2D, firstRoom: first);
        }

        private void InitializeTraps(RoomRunner scriptRoom)
        {
            if (Random.value >= scriptRoom.TrapBuildChance)
                BuildRandomTrap(scriptRoom);
        }

        private void InitializeDecor(RoomRunner scriptRoom)
        {
/*            for (int i = 0; i < length; i++)
            {

            }*/
            if (Random.value >= scriptRoom.DecorBuildChance)
                BuildRandomDecor(scriptRoom);
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

        private void BuildRandomDecor(RoomRunner room)
        {
            int decorsCount = room.DecorContainer.Count;
            if (decorsCount == 0)
                return;

            Instantiate(room.DecorContainer[Random.Range(0, decorsCount)],
                        room.decorSpotsContainer.GetChild(Random.Range(0, room.decorSpotsContainer.childCount)));
        }

        private RoomData GetNextRoom() => RoomPrefabs[0/*Random.Range(0, RoomPrefabs.Count)*/].GetComponent<RoomData>();

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
