using Infrastructure;
using UnityEngine;
using System;

namespace Logic
{
    public class RoomsRun : MonoBehaviour
    {
        private readonly InputService _input = InputService.Instance;

        [SerializeField] private float _speed;

        [Header("Set the length of the room for spawner")] 
        public float Length;
        
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
        private bool _enabled = false;

        public event Action OutOfBoundsEvent;

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

        }

        private void RunRoom()
        {
            transform.position += _horizontalVelocity * Time.deltaTime;
        }
    }
}
