using System;
using UnityEngine;

namespace Logic.Actors
{
    public class Unit : MonoBehaviour
    {
        public event Action DiedEvent;

        public void Die()
        {
            DiedEvent?.Invoke();

            Debug.Log("omg i'm dying");
        }
    }
}