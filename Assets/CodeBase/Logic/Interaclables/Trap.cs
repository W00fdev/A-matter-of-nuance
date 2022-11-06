using UnityEngine;
using Logic.Actors;

namespace Logic.Interactables
{
    public class Trap : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float fakeChance;

        public void Prepare(GameObject unit)
        {
            return;
        }

        public void Enable(GameObject unit)
        {
            if (Random.value >= fakeChance)
                Debug.Log("fake");
            else
                unit.GetComponent<Unit>().Die();
        }
    }
}