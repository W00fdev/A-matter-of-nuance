using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class RandomizedCycle<T>
    {
        private Queue<T> _queue = new();
        public readonly T[] data;

        public RandomizedCycle(T[] data)
        {
            this.data = data;
            Requeue();
        }

        public T GetNext()
        {
            if (_queue.Count == 0)
                Requeue();

            return _queue.Dequeue();
        }

        private void Requeue()
        {
            List<int> used = new();
            _queue = new Queue<T>(data.Length);

            while (_queue.Count != data.Length)
            {
                int nextId = Random.Range(0, data.Length);

                if (used.Contains(nextId))
                    continue;

                _queue.Enqueue(data[nextId]);
                used.Add(nextId);
            }
        }
    }
}