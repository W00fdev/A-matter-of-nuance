using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(AudioSource))]
    public class RoomWalkAudio : MonoBehaviour
    {
        [SerializeField]
        private RoomsSpawner _roomsSpawner;

        private AudioSource _walkSound;

        private Vector3 _lastRunnerPosition;

        private void Awake() => _walkSound = GetComponent<AudioSource>();

        private void LateUpdate()
        {
            if (_roomsSpawner.LastRoom == null)
                return;

            if (_lastRunnerPosition != _roomsSpawner.LastRoom.transform.position && _lastRunnerPosition != Vector3.zero)
            {
                if (!_walkSound.isPlaying)
                    _walkSound.Play();
            }
            else
            {
                if (_walkSound.isPlaying)
                    _walkSound.Stop();
            }

            _lastRunnerPosition = _roomsSpawner.LastRoom.transform.position;
        }
    }
}