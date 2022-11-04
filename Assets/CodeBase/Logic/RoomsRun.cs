using Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class RoomsRun : MonoBehaviour
    {
        private readonly InputService _input = InputService.Instance;

        [SerializeField] private float _speed;

        public bool Enabled = false;
        
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

        private void Start()
        {
            _horizontalVelocity = new Vector3(-Speed, 0f, 0f);
        }

        private void Update()
        {
            if (Enabled == false)
                return;

            if (_input.IsRunButton())
            {
                RunRoom();
                CheckBounds();
            }
        }

        private void CheckBounds()
        {

        }

        private void RunRoom()
        {
            transform.position += _horizontalVelocity * Time.deltaTime;
        }
    }
}
