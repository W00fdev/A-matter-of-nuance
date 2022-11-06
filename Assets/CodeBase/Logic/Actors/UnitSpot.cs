using UnityEngine;

namespace Logic.Actors
{
    public class UnitSpot : MonoBehaviour
    {
        public bool IsKing => transform.parent.GetChild(0) == transform;
        public bool CanBeTraitor => transform.parent.GetChild(1) == transform;
    }
}