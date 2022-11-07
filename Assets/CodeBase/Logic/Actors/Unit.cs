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

        [HideInInspector]
        public bool noAllowing = false;

        private SpriteRenderer spriteRenderer;
        private bool blockMove = false;

        private bool _savedMovementBeforeBlock;
        private bool _allowedMovementBeforeBlock;

        public AudioSource fall;
        public AudioSource blood;
        public AudioSource walk;
        public AudioSource died;

        private void Awake() => _animator = GetComponent<Animator>();
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (!Constants.IsGameStarted)
                return;

            if (Input.GetMouseButtonUp(0))
                walk.Stop();
            else
            if (Input.GetMouseButtonDown(0) && Constants.AllowedMovement)
                walk.Play();

            if (!isKing)
                return;

            if (blockMove)
                return;

            TraitorManager.isFreezed = Input.GetMouseButton(1);

            /*            if (!noAllowing)
                            Constants.AllowedMovement = !Input.GetMouseButton(1);*/
            
            if (!noAllowing)
                AllowedMovementByBlock();

            spriteRenderer.flipX = Input.GetMouseButton(1);
        }

        private void AllowedMovementByBlock()
        {
            bool blockInput = Input.GetMouseButton(1);
            bool blockInputReleased = Input.GetMouseButtonUp(1);

            if (blockInput == true && _savedMovementBeforeBlock == false)
            {
                _allowedMovementBeforeBlock = Constants.AllowedMovement;
                _savedMovementBeforeBlock = true;

                Constants.AllowedMovement = false;
            } 
            else 
            if (blockInputReleased == true && _savedMovementBeforeBlock == true)
            {
                Constants.AllowedMovement = _allowedMovementBeforeBlock;
                _savedMovementBeforeBlock = false;
            }
        }

        public void NO(bool value)
        {
            //if (_animator.GetBool("no") && value)
            //    Run();

            _animator.SetBool("no", value);
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
            died.Play();
        }

        public void Betray()
        {
            _animator.SetTrigger("blood");
            blood.Play();
        }

        public void Die()
        {
            blockMove = true;
            Constants.AllowedMovement = false;

            if (_animator != null)
                _animator.SetTrigger("fall");

            fall.Play();
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

            if (_lastCloud != null)
                _lastCloud.gameObject.SetActive(false);
            blockMove = false;
        }

        public void Run() => _animator.SetTrigger("run");
        public void Destroy() => Destroy(gameObject);
    }
}