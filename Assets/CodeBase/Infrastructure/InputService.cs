using UnityEngine;

namespace Infrastructure
{
    public class InputService
    {
        private static InputService _instance;
        public static InputService Instance => _instance ??= new InputService();

        public bool IsRunButton() => Input.GetMouseButton(0);
        public bool IsAttackButton() => Input.GetMouseButton(1);
    }
}
