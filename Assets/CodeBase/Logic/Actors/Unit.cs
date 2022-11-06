using System;
using UnityEngine;

namespace Logic.Actors
{
    public class Unit : MonoBehaviour
    { 
        public event Action DiedEvent;

        public RoomsSpawner roomsSpawner;

        private Animator _animator;
        private Unit _lastVictim;
        private static Transform _lastCloud;

        private void Awake() => _animator = GetComponent<Animator>();

        public void Kill(Unit victim, Transform cloud)
        {
            _animator.SetTrigger("betray");
            _lastVictim = victim;
            _lastCloud = cloud;
        }

        public void OnBetrayal()
        {
            if (_lastVictim == null)
                throw new UnityException("no victim");

            _lastCloud.gameObject.SetActive(true);
            _lastVictim.Betray();
        }

        public void Betray() => _animator.SetTrigger("blood");

        public void Die()
        {
            Constants.AllowedMovement = false;
            _animator.SetTrigger("fall");
            DiedEvent?.Invoke();
        }

        public void SpriteClone()
        {
            var clone = Instantiate(gameObject, transform.position, Quaternion.identity, roomsSpawner.LastRoom.transform);
            Destroy(clone.GetComponent<Unit>());
            Destroy(clone.GetComponent<BoxCollider>());
            Destroy(clone.GetComponent<Rigidbody>());
            Destroy(clone.GetComponent<Animator>());
            clone.transform.position = (Vector2)clone.transform.position;
            clone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            clone.GetComponent<SpriteRenderer>().sortingOrder = 4;
            Constants.AllowedMovement = true;
            _lastCloud.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            return;
        }
    }
}