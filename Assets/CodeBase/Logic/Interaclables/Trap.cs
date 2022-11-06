using UnityEngine;
using Logic.Actors;

namespace Logic.Interactables
{
    public class Trap : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float fakeChance;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Prepare(GameObject unit)
        {
            if (_animator != null)
                _animator.SetTrigger("Prepare");
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