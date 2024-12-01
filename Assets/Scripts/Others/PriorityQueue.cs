using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<KeyValuePair<T, int>> elements = new List<KeyValuePair<T, int>>();

    // Add an item with a priority
    public void Enqueue(T item, int priority)
    {
        elements.Add(new KeyValuePair<T, int>(item, priority));
    }

    // Remove and return the item with the highest priority
    public T Dequeue()
    {
        if (elements.Count == 0)
            throw new System.InvalidOperationException("The queue is empty.");

        int bestIndex = 0;
        for (int i = 1; i < elements.Count; i++)
        {
            if (elements[i].Value < elements[bestIndex].Value)
                bestIndex = i;
        }

        T bestItem = elements[bestIndex].Key;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }

    // Check if the queue is empty
    public bool IsEmpty => elements.Count == 0;

    // Get the number of items in the queue
    public int Count => elements.Count;

    // Update the priority of an item
    public void UpdatePriority(T item, int newPriority)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(elements[i].Key, item))
            {
                elements[i] = new KeyValuePair<T, int>(item, newPriority);
                return;
            }
        }

        throw new System.ArgumentException("Item not found in the priority queue.");
    }

    // Delete a specific item from the queue
    public void Delete(T item)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(elements[i].Key, item))
            {
                elements.RemoveAt(i);
                return;
            }
        }

        throw new System.ArgumentException("Item not found in the priority queue.");
    }
}
