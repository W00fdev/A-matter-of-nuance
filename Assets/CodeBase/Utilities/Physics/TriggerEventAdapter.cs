using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Physics
{
    public class TriggerEventAdapter : MonoBehaviour
    {
        public UnityEvent<GameObject> onEnter;

        private void OnTriggerEnter(Collider other) => onEnter.Invoke(other.gameObject);
    }
}