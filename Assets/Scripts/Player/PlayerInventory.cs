using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory<T>
{
    private List<T> _Inventory = new List<T>();
    public int Capacity;
    public int Count
    {
        get { return _Inventory.Count; }
    }

    public PlayerInventory()
    {
        Capacity = 10;
    }

    public bool Remove(T item)
    {
        if (!_Inventory.Contains(item))
        {
            return false;
        }
        else
        {
            _Inventory.Remove(item);
            return true;
        }
    }

    public void RemoveAll()
    {
        foreach (T item in _Inventory)
        {
            _Inventory.Remove(item);
        }
    }

    public bool Add(T item)
    {
        if (_Inventory.Count < Capacity)
        {
            _Inventory.Add(item);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Contains(T item)
    {
        return _Inventory.Contains(item);
    }

    public override string ToString()
    {
        string result = "";
        if (_Inventory.Count == 0)
        {
            result = "Empty  ";
        }
        else
        {
            foreach (T item in _Inventory)
            {
                result += item.ToString() + ", ";
            }
        }
        result = result.Substring(0, result.Length - 2);
        return result;
    }
}
