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

    public virtual T GetNext()
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

public class RandomizedCycleScrolls 
{
    private Queue<ScrollData> _queue = new();
    private Queue<ScrollData> _queueAdditional = new();

    public new readonly ScrollData[] data;

    public RandomizedCycleScrolls(ScrollData[] data) 
    {
        this.data = data;
        Requeue();
    }

    public ScrollData GetNext()
    {
        if (_queue.Count == 0)
            Requeue();

        if (_queueAdditional.Count != 0)
        {
            if (Constants.VassalsCount > 0)
            {
                if (Random.value <= 0.15f)
                    return _queueAdditional.Dequeue();
            }
        }

        return _queue.Dequeue();
    }

    protected void Requeue()
    {
        List<int> used = new();

        _queue = new Queue<ScrollData>(data.Length);
        _queueAdditional.Clear();

        int actualLength = data.Length;

        while (_queue.Count != actualLength)
        {
            int nextId = Random.Range(0, data.Length);

            if (used.Contains(nextId))
                continue;

            if (data[nextId].NeedVassal == true)
            {
                _queueAdditional.Enqueue(data[nextId]);
                used.Add(nextId);
                actualLength--;
                continue;
            }

            _queue.Enqueue(data[nextId]);
            used.Add(nextId);
        }
    }
}