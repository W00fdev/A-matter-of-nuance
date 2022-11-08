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
        private Unit lastUnit;

        private void Awake()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
        }

        public void Prepare(GameObject unit)
        {
            lastUnit = null;
            _animator.SetTrigger("Prepare");
            _animator.SetBool("isFake", Random.value >= fakeChance);
        }

        public void Enable(GameObject unit)
        {
            if (_animator.GetBool("isFake"))
                enabled = false;
            else
                lastUnit = unit.GetComponent<Unit>();
        }

        public void AnimKill()
        {
            if (lastUnit == null)
                return;

            lastUnit.Die();
            lastUnit = null;
            enabled = false;
        }
    }
}