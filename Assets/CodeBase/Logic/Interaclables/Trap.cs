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
                Debug.Log("sha");
                lastUnit = unit.GetComponent<Unit>();
                //unitUnit.noAllowing = true;
                //lastUnit.BlockMove();

                if (_animator == null)
                    lastUnit.Die();

                if (_animator != null)
                    _animator.SetTrigger("Attack");
            }
        }

        private Unit lastUnit;

        public void AnimKill()
        {
            Debug.Log("boom");
            lastUnit.Die();
        }
    }
}