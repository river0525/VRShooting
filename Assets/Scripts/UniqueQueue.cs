using System;
using System.Collections.Generic;

public class UniqueQueue<T>
{
    private readonly Queue<T> queue = new Queue<T>();
    private readonly HashSet<T> set = new HashSet<T>();

    public int Count => queue.Count;

    public bool Enqueue(T item)
    {
        if (set.Contains(item)) return false;
        queue.Enqueue(item);
        set.Add(item);
        return true;
    }

    public T Dequeue()
    {
        T item = queue.Dequeue();
        set.Remove(item);
        return item;
    }

    public bool Contains(T item)
    {
        return set.Contains(item);
    }

    public void Clear()
    {
        queue.Clear();
        set.Clear();
    }

    public T Peek()
    {
        return queue.Peek();
    }
}
