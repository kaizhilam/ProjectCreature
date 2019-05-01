using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager: MonoBehaviour
{ 
    public static InventoryManager Instance { get; private set; }
    private static SlottedItem[] InventoryItems;
    private static SlottedItem[] HotbarItems;

    public InventoryManager()
    {

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Swap(SlottedItem item1, SlottedItem item2)
    {
        bool isInHotbar1 = IsInHotbar(item1);
        bool isInHotbar2 = IsInHotbar(item2);
        if(isInHotbar1 && isInHotbar2)
        {
            int hotbarindex1 = GetHotbarIndex(item1);
            int hotbarindex2 = GetHotbarIndex(item2);
            SlottedItem temp = item1;
            HotbarItems[hotbarindex1] = item2;
            HotbarItems[hotbarindex2] = temp;
        }
    }

    public void Remove(SlottedItem item)
    {

    }

    public void Add(SlottedItem item)
    {

    }

    public int GetIndex(SlottedItem item)
    {

    }

    public int GetHotbarIndex(SlottedItem item)
    {

    }

    public bool IsInHotbar(SlottedItem item)
    {

    }
    public SlottedItem GetItem(int index)
    {

    }

    public SlottedItem GetHotbarItem(int index)
    {

    }

    public void PopulateArrayFromSceneObject()
    {

    }
}

    