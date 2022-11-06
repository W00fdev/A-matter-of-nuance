using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "New Room", menuName = "Room Data", order = 51)]
    public class RoomData : ScriptableObject
    {
        public GameObject RoomPrefab;
        public float Length;
    }
}
