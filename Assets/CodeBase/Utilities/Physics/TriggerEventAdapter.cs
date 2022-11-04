using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Physics
{
    public class TriggerEventAdapter : MonoBehaviour
    {
        public LayerMask layerMask;
        public UnityEvent<GameObject> onEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (layerMask.value == 1 << other.gameObject.layer)
                onEnter.Invoke(other.gameObject);
        }
    }
}