using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Physics
{
    public class TriggerEventAdapter : MonoBehaviour
    {
        public UnityEvent<GameObject> onEnter;
        public UnityEvent<GameObject> onExit;

        private void OnTriggerEnter(Collider other) => onEnter.Invoke(other.gameObject);
        private void OnTriggerExit(Collider other) => onExit.Invoke(other.gameObject);
    }
}