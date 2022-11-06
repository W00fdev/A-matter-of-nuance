using UnityEngine;
using Logic.Actors;
using System.Collections;

namespace Logic.Interactables
{
    public class Trap : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float fakeChance;

        public Animator _animator;

        private void Awake()
        {
            if (_animator == null)
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
            {
                StartCoroutine(LateKill(unit.GetComponent<Unit>()));
                
                if (_animator != null)
                    _animator.SetTrigger("Attack");
            }
        }

        private IEnumerator LateKill(Unit unit)
        {
            yield return new WaitForSeconds(Constants.PingBeforeTrapDie);
            unit.Die();
        }
    }
}