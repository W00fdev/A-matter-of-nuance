using System;
using UnityEngine;

namespace Logic.Actors
{
    public class Unit : MonoBehaviour
    {
        public bool isKing;

        public event Action DiedEvent;

        public RoomsSpawner roomsSpawner;
        public TraitorManager traitorManager;

        private Animator _animator;
        private Unit _lastVictim;
        private static Transform _lastCloud;

        private Vector3 defaultscale;
        private bool blockMove = false;
        private void Awake() => _animator = GetComponent<Animator>();
        private void Start() => defaultscale = transform.localScale;

        private void Update()
        {
            if (!isKing)
                return;

            if (blockMove)
                return;

            if (traitorManager.isFreezed && !Input.GetMouseButton(2))
                traitorManager.isFreezed = false;

            if (Input.GetMouseButton(2))
                Constants.AllowedMovement = false;
            else
                Constants.AllowedMovement = true;

            transform.localScale = Input.GetMouseButton(2) ? new Vector3(-defaultscale.x, defaultscale.y, defaultscale.z) : defaultscale;

            //Unit traitor = traitorManager.GetTraitor();

            //if (traitor != null && !traitorManager.isFreezed)
            //    traitor.Scare();
        }

        public void Scare()
        {
            traitorManager.isFreezed = true;
        }

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
            blockMove = true;
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
            blockMove = false;
        }

        public void Run() => _animator.SetTrigger("run");
        public void Destroy() => Destroy(gameObject);
    }
}