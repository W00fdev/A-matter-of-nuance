using UnityEngine;

namespace Infrastructure
{
    public class InputService
    {
        private static InputService _instance;
        public static InputService Instance => _instance ??= new InputService();
        
        // i'm tired of left mouse button
        public bool IsRunButton() => true;//Input.GetMouseButton(0);
        public bool IsAttackButton() => Input.GetMouseButton(1);

        public bool IsLeftMouseButtonDown() => Input.GetMouseButtonDown(0);
    }
}
