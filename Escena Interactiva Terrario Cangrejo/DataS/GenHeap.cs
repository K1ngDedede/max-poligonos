using System;
using System.Net.Http.Headers;
using UnityEngine;

/// <summary>
/// Array-based Heap implementation.
/// </summary>
/// TOMADO DE: https://gist.github.com/roufamatic/ee7e11469809f2b276c0d3dc6b8dd80b
public class Heap<GameObject>
{
    private enum HeapType
    {
        Min,
        Max
    };

    private readonly HeapType _heapType;
    UnityEngine.GameObject[] _heap;
    int _count;
    private Transform crabPos;

    /// <summary>
    /// Create a new heap.
    /// </summary>
    /// <param name="minSize">The minimum number of elements the heap is expected to hold.</param>
    /// <param name="isMaxHeap">If "true", this is a Max Heap, where the largest values rise to the top. Otherwise, this is a Min Heap.</param>
    public Heap(int minSize, bool isMaxHeap = false)
    {
        _heapType = isMaxHeap ? HeapType.Max : HeapType.Min;
        _heap = new UnityEngine.GameObject[((int)Math.Pow(2, Math.Ceiling(Math.Log(minSize, 2))))];
        crabPos = null;
    }

    /// <summary>
    /// Current size of the Heap.
    /// </summary>
    public int Count { get { return _count; } }

    /// <summary>
    /// Test to see if the Heap is empty.
    /// </summary>
    public bool IsEmpty { get { return _count == 0; } }

    /// <summary>
    /// Add a new value to the Heap.
    /// </summary>
    /// <param name="val"></param>
    public void Insert(UnityEngine.GameObject val)
    {
        if (_count == _heap.Length)
        {
            DoubleHeap();
        }
        _heap[_count] = val;
        ShiftUp(_count);
        _count++;
    }

    /// <summary>
    /// View the value currently at the top of the Heap.
    /// </summary>
    /// <returns></returns>
    public UnityEngine.GameObject Peek()
    {
        if (_heap.Length == 0) throw new ArgumentOutOfRangeException("No values in heap");
        return _heap[0];
    }

    /// <summary>
    /// Remove the value currently at the top of the Heap and return it.
    /// </summary>
    /// <returns></returns>
    public UnityEngine.GameObject Remove()
    {
        UnityEngine.GameObject output = Peek();
        _count--;
        _heap[0] = _heap[_count];
        ShiftDown(0);
        return output;
    }

    /// <summary>
    /// Move an element up the Heap.
    /// </summary>
    /// <param name="heapIndex"></param>
    private void ShiftUp(int heapIndex)
    {
        if (heapIndex == 0) return;
        int parentIndex = (heapIndex - 1) / 2;
        bool shouldShift = DoCompare(parentIndex, heapIndex) > 0;
        if (!shouldShift) return;
        Swap(parentIndex, heapIndex);
        ShiftUp(parentIndex);
    }

    /// <summary>
    /// Move an element down the Heap.
    /// </summary>
    /// <param name="heapIndex"></param>
    private void ShiftDown(int heapIndex)
    {
        int child1 = heapIndex * 2 + 1;
        if (child1 >= _count) return;
        int child2 = child1 + 1;

        int preferredChildIndex = (child2 >= _count || DoCompare(child1, child2) <= 0) ? child1 : child2;
        if (DoCompare(preferredChildIndex, heapIndex) > 0) return;
        Swap(heapIndex, preferredChildIndex);
        ShiftDown(preferredChildIndex);
    }

    /// <summary>
    /// Swap two items in the underlying array.
    /// </summary>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    private void Swap(int index1, int index2)
    {
        UnityEngine.GameObject temp = _heap[index1];
        _heap[index1] = _heap[index2];
        _heap[index2] = temp;
    }

    /// <summary>
    /// Perform a comparison between two elements of the heap.
    /// This method encapsulates the min/max behavior of the heap so the rest of the class can remain blissfully ignorant.
    /// </summary>
    /// <param name="initialIndex"></param>
    /// <param name="contenderIndex"></param>
    /// <returns></returns>
    protected int DoCompare(int initialIndex, int contenderIndex)
    {
        UnityEngine.GameObject initial = _heap[initialIndex];
        UnityEngine.GameObject contender = _heap[contenderIndex];
        int value = CompareTo(initial, contender);
        if (_heapType == HeapType.Max) value = -value;
        return value;
    }

    /// <summary>
    /// Increase the size of the underlying storage.
    /// </summary>
    private void DoubleHeap()
    {
        var copy = new UnityEngine.GameObject[_heap.Length * 2];
        for (int i = 0; i < _heap.Length; i++)
        {
            copy[i] = _heap[i];
        }
        _heap = copy;
    }

    private int CompareTo(UnityEngine.GameObject i, UnityEngine.GameObject c)
    {
        float d1 = Vector3.Distance(i.transform.position, crabPos.position);
        float d2 = Vector3.Distance(c.transform.position, crabPos.position);
        return (int)(d1-d2);
    }

    public void updateCrab(Transform crav)
    {
        crabPos = crav;
    }
}