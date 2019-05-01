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
        if(!isInHotbar1 && !isInHotbar2)
        {
            int invIndex1 = GetIndex(item1);
            int invIndex2 = GetIndex(item2);
            SlottedItem temp = item1;
            InventoryItems[invIndex1] = item2;
            InventoryItems[invIndex2] = temp;
        }
        else if(isInHotbar1 && !isInHotbar2)
        {
            int hotbarindex1 = GetHotbarIndex(item1);
            int invIndex1 = GetIndex(item2);
            SlottedItem temp = item1;
            HotbarItems[hotbarindex1] = item2;
            InventoryItems[invIndex1] = temp;
        }
        else if(!isInHotbar1 && isInHotbar2)
        {
            int invIndex1 = GetIndex(item1);
            int hotbarindex1 = GetHotbarIndex(item2);
            SlottedItem temp = item1;
            InventoryItems[invIndex1] = item2;
            HotbarItems[hotbarindex1] = temp;
        }
        else
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
        int index = -1;
        if (IsInHotbar(item))
        {
            index = GetHotbarIndex(item);
            HotbarItems[index] = null;
        }
        else
        {
            index = GetIndex(item);
            InventoryItems[index] = null;
        }

    }

    public bool AddToFirstInvSlot(SlottedItem item)
    {
        for (int i = 0; i < InventoryItems.Length; i++)
        {
            if (InventoryItems[i] != null)
            {
                InventoryItems[i] = item;
                return true;
            }
        }
        return false;
    }

    public bool AddToInventoryIndex(SlottedItem item, int index)
    {
        if(InventoryItems[index] != null)
        {
            InventoryItems[index] = item;
            return true;
        }
        return false;
    }

    public int GetIndex(SlottedItem item)
    {
        for (int i = 0; i < InventoryItems.Length; i++)
        {
            if(item == InventoryItems[i])
            {
                return i;
            }
        }
        return -1;
    }

    public int GetHotbarIndex(SlottedItem item)
    {
        for (int i = 0; i < HotbarItems.Length; i++)
        {
            if(item == HotbarItems[i])
            {
                return i;
            }
        }
        return -1;
    }

    public bool IsInHotbar(SlottedItem item)
    {
        for (int i = 0; i < HotbarItems.Length; i++)
        {
            if (item == HotbarItems[i])
            {
                return true;
            }
        }
        return false;
    }
    public SlottedItem GetItem(int index)
    {
        return InventoryItems[index];
    }

    public SlottedItem GetHotbarItem(int index)
    {
        return HotbarItems[index];
    }

    public void PopulateArrayFromSceneObject()
    {

    }
}

    