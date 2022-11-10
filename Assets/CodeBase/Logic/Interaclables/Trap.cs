using UnityEngine;
using Logic.Actors;
using System.Collections;

namespace Logic.Interactables
{
    public class Trap : MonoBehaviour
    {
        private enum State { Initial, Prepared, Enabled }

        [Range(0f, 1f)]
        public float fakeChance;

        public Animator _animator;
        private Unit lastUnit;
        private State _state;

        private void Awake()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();

            _state = State.Initial;
        }

        public void Prepare(GameObject unit)
        {
            if (_state != State.Initial)
                return;

            lastUnit = null;
            _animator.SetTrigger("Prepare");
            _animator.SetBool("isFake", Random.value < fakeChance);
            _state = State.Prepared;
        }

        public void Enable(GameObject unit)
        {
            if (_state != State.Prepared)
                return;

            if (_animator.GetBool("isFake"))
                enabled = false;
            else
                lastUnit = unit.GetComponent<Unit>();

            _state = State.Enabled;
        }

        public void Forget() => lastUnit = null;
        public void AnimKill()
        {
            if (lastUnit != null)
                lastUnit.Die();

            Destroy(this);
        }
    }
}