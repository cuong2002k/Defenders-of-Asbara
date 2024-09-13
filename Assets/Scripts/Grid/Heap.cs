using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T : IHeapItem<T>
{
    private T[] _items;
    private int _currentHeapSize;
    public int Count => this._currentHeapSize;
    public Heap(int maxSize)
    {
        this._items = new T[maxSize];
        _currentHeapSize = 0;
    }
    //get parent index
    private int Parent(int index)
    {
        return (index - 1) / 2;
    }
    //get left index
    private int Left(int index)
    {
        return 2 * index + 1;
    }
    // get right index
    private int Right(int index)
    {
        return 2 * index + 2;
    }
    // insert
    public void Add(T item)
    {
        item.HeapIndex = this._currentHeapSize;
        this._items[this._currentHeapSize] = item;
        // sort up
        SortUp(item);
        this._currentHeapSize++;
    }
    // get first
    public T GetFirst()
    {
        return this._items[0];
    }
    // rm first
    public void RemoveFirst()
    {
        this._currentHeapSize--;
        this._items[0] = this._items[this._currentHeapSize];
        this._items[0].HeapIndex = 0;
        SortDown(this._items[0]);
    }
    // sortup
    private void SortUp(T item)
    {
        int parentIndex = this.Parent(item.HeapIndex);
        while (true)
        {
            T parentItem = this._items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                //swap
                Swap(item, parentItem);
            }
            else
            {
                break;
            }
            parentIndex = this.Parent(item.HeapIndex);
        }
    }
    // sortdown
    private void SortDown(T item)
    {
        while (true)
        {
            int leftChildIndex = this.Left(item.HeapIndex);
            int rightChildIndex = this.Right(item.HeapIndex);
            int swapIndex = 0;
            if (leftChildIndex < this._currentHeapSize)
            {
                swapIndex = leftChildIndex;
                if (rightChildIndex < this._currentHeapSize)
                {
                    if (this._items[leftChildIndex].CompareTo(this._items[rightChildIndex]) < 0)
                    {
                        swapIndex = rightChildIndex;
                    }
                }

                if (item.CompareTo(this._items[swapIndex]) < 0)
                {
                    this.Swap(item, this._items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

    }
    // delete first node
    //swap
    private void Swap(T itemA, T itemB)
    {
        this._items[itemA.HeapIndex] = itemB;
        this._items[itemB.HeapIndex] = itemA;
        int heapIndexA = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = heapIndexA;
    }

    public bool Contains(T item)
    {
        return Equals(_items[item.HeapIndex], item);
    }

    public void Update(T item)
    {
        SortUp(item);
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    public int HeapIndex { get; set; }
}
