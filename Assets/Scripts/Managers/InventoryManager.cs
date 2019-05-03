using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager: MonoBehaviour
{
    private float targetAlpha = 1;
    private float smoothing = 4;
    private CanvasGroup canvasGroup;
    public static InventoryManager Instance { get; private set; }
    private static SlottedItem[] InventoryItems;
    private static SlottedItem[] HotbarItems;
    public static Slot[] Slots;

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

    public virtual void Start()
    {
        Slots = GetComponentsInChildren<Slot>();
        print(Slots.Length);
        canvasGroup = GetComponent<CanvasGroup>();
        InventoryItems = new SlottedItem[Slots.Length];
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

    //returns index item was added to, returns -1 if no room for it
    public int AddToFirstInvSlot(SlottedItem item)
    {
        for (int i = 0; i < InventoryItems.Length; i++)
        {
            if (InventoryItems[i] == null)
            {
                InventoryItems[i] = item;
                return i;
            }
        }
        return -1;
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

    public void RefreshInventoryFromList()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            RefreshSlotFromList(i);
        }
    }

    public void RefreshSlotFromList(int index)
    {
        print("refreshing slot " + index);
        Slots[index]?.GetComponent<ItemUI>()?.SetItem(InventoryItems[index]);
        
    }

    public void DisplaySwitch()
    {
        if (targetAlpha == 0)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    public void Show()
    {
        canvasGroup.blocksRaycasts = true;
        targetAlpha = 1;
    }
    public void Hide()
    {
        canvasGroup.blocksRaycasts = false;
        targetAlpha = 0;
    }

    private void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < .01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }
}

    