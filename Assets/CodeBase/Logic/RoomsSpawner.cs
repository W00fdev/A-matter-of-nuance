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
    }
}
