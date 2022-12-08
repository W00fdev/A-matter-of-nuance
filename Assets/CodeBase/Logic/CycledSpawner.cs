using Data;
using Logic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RandomizedCycle<T>

{
    protected Queue<T> _queue = new();
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

    protected virtual void Requeue()
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

public sealed class RandomizedCycleScrolls : RandomizedCycle<ScrollData>
{
    public RandomizedCycleScrolls(ScrollData[] data) : base(data)
    {
    }

    protected override void Requeue()
    {
        List<int> used = new();

        _queue = new Queue<ScrollData>(data.Length);

        while (_queue.Count != data.Length)
        {
            int nextId = Random.Range(0, data.Length);

            if (used.Contains(nextId))
                continue;

            if (Constants.VassalsCount == 0)
                if (data[nextId].NeedVassal == true)
                    continue;

            _queue.Enqueue(data[nextId]);
            used.Add(nextId);
        }
    }
}