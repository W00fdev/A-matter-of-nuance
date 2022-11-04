using Infrastructure;
using UnityEngine;
using System;

namespace Logic
{
    public class RoomRunner : MonoBehaviour
    {
        private readonly InputService _input = InputService.Instance;

        [SerializeField] private float _speed;

        [HideInInspector] public float FirstOffset; // Sets at spawn
        [HideInInspector] public float CameraBoundsX; // Sets at spawn
        
        [Header("Set the length of the room for spawner")] 
        public float Length; // Sets at spawn (by scriptable object)

        public float Speed
        {
            get => _speed;
            set 
            {
                _speed = value;
                _horizontalVelocity.x = -(value);
            }
        }

        private Vector3 _horizontalVelocity;
        private float _distance = 0f;
        private bool _enabled = false;
        private bool _outOfBounds;

        public event Action OutOfBoundsEvent;
        
        private const float DeathOffsetX = 1.5f;

        private void Awake()
        {
            _horizontalVelocity = new Vector3(-Speed, 0f, 0f);
        }

        private void Update()
        {
            if (_enabled == false)
                return;

            if (_input.IsRunButton())
            {
                RunRoom();
                CheckBounds();
            }
        }

        public void Enable() => _enabled = true;
        public void Disable() => _enabled = false;

        private void CheckBounds()
        {
            if (_distance + FirstOffset >= Length && _outOfBounds == false)
            {
                OutOfBoundsEvent?.Invoke();
                Debug.Log("Out of bounds");
                _outOfBounds = true;
            }

            if (_distance + FirstOffset >= Length + CameraBoundsX + DeathOffsetX)
            {
                Debug.Log("Bye-bye");
                Destroy(gameObject);
            }
        }

        private void RunRoom()
        {
            Vector3 runOffset = _horizontalVelocity * Time.deltaTime;
            transform.position += runOffset;
            _distance += Mathf.Abs(runOffset.x);
        }
    }
}
