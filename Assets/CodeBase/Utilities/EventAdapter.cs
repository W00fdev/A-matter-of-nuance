using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class EventAdapter : MonoBehaviour
    {
        public UnityEvent onHandle;

        public void Handle() => onHandle.Invoke();
    }
}