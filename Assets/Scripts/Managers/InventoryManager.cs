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
    public static Slot[] HotbarSlots;

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
        GameObject.Find("Player").GetComponent<InputManager>().BKey += DisplaySwitch;
        Slots = GetComponentsInChildren<Panel>()[1].GetComponentsInChildren<Slot>();
        HotbarSlots = GetComponentsInChildren<Panel>()[0].GetComponentsInChildren<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
        InventoryItems = new SlottedItem[Slots.Length];
        HotbarItems = new SlottedItem[HotbarSlots.Length];
        DisplaySwitch();
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
            int hotbarindex1 = GetFirstHotbarIndexOfItem(item1);
            int invIndex1 = GetIndex(item2);
            SlottedItem temp = item1;
            HotbarItems[hotbarindex1] = item2;
            InventoryItems[invIndex1] = temp;
        }
        else if(!isInHotbar1 && isInHotbar2)
        {
            int invIndex1 = GetIndex(item1);
            int hotbarindex1 = GetFirstHotbarIndexOfItem(item2);
            SlottedItem temp = item1;
            InventoryItems[invIndex1] = item2;
            HotbarItems[hotbarindex1] = temp;
        }
        else
        {
            int hotbarindex1 = GetFirstHotbarIndexOfItem(item1);
            int hotbarindex2 = GetFirstHotbarIndexOfItem(item2);
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
            index = GetFirstHotbarIndexOfItem(item);
            HotbarItems[index] = null;
        }
        else
        {
            index = GetIndex(item);
            InventoryItems[index] = null;
        }

    }

    public int FirstEmptySlotInvIndex()
    {
        for (int i = 0; i < InventoryItems.Length; i++)
        {
            if (InventoryItems[i] == null)
            {
                return i; 
            }
        }
        return -1;
    }

    public int FirstEmptySlotHotbarIndex()
    {
        for (int i = 0; i < HotbarItems.Length; i++)
        {
            if(HotbarItems[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    //returns index item was added to, returns false if no room for it
    public int PutIntoUI(SlottedItem item)
    {
        int indexHotbar = GetFirstHotbarIndexOfItem(item);
        int indexInv = GetIndex(item);
        int firstEmpty = FirstEmptySlotInvIndex();
        int firstEmptyHotbar = FirstEmptySlotHotbarIndex();
        print(indexHotbar + " " + indexInv + " " + firstEmpty + " " + firstEmptyHotbar);
        //check if item in hotbar already
        if (indexHotbar > -1)
        {
            List<int> indexes = GetHotbarIndexesOfItem(item);
            //if can hold it, ++
            print(HotbarSlots[indexHotbar].name);
            for (int i = 0; i < indexes.Count; i++)
            {
                if (HotbarItems[indexes[i]].count < HotbarItems[indexes[i]].capacity)
                {
                    HotbarItems[indexes[i]].count++;
                    RefreshHotbarSlotFromList(indexes[i]);
                    return indexes[i];
                }
            }
            
        }

        //then check if its in inventory already
        if(indexInv > -1)
        {
            List<int> indexes = GetInvIndexesOfItem(item);
            //if can hold it, ++
            for (int i = 0; i < indexes[i]; i++)
            {
                if (HotbarItems[indexes[i]].count < InventoryItems[indexes[i]].capacity)
                {
                    HotbarItems[indexes[i]].count++;
                    RefreshSlotFromList(indexes[i]);
                    return indexes[i];
                }
            }
            
        }

        //then check for free spot in hotbar
        if(firstEmptyHotbar > -1)
        {
            HotbarItems[firstEmptyHotbar] = item;
            RefreshHotbarSlotFromList(firstEmptyHotbar);
            return firstEmptyHotbar;
        }
        //then check for free spot in inventory
        if (firstEmpty > -1)
        {
            InventoryItems[firstEmpty] = item;
            RefreshSlotFromList(firstEmpty);
            return firstEmpty;
        }
        //no room for item anywhere, return false
        return -1;


    }
    //outdated, dont use
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
            if(item.objID == InventoryItems[i]?.objID)
            {
                return i;
            }
        }
        return -1;
    }

    public int GetFirstHotbarIndexOfItem(SlottedItem item)
    {
        for (int i = 0; i < HotbarItems.Length; i++)
        {
            if(item.objID == HotbarItems[i]?.objID)
            {
                return i;
            }
        }
        return -1;
    }

    public List<int> GetHotbarIndexesOfItem(SlottedItem item)
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < HotbarItems.Length; i++)
        {
            if(item.objID == HotbarItems[i]?.objID)
            {
                indexes.Add(i);
            }
        }
        return indexes;
    }

    public List<int> GetInvIndexesOfItem(SlottedItem item)
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < InventoryItems.Length; i++)
        {
            if (item.objID == InventoryItems[i]?.objID)
            {
                indexes.Add(i);
            }
        }
        return indexes;
    }

    public bool IsInHotbar(SlottedItem item)
    {
        for (int i = 0; i < HotbarItems.Length; i++)
        {
            if (item.objID == HotbarItems[i].objID)
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

    public void RefreshInventoryFromList()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            RefreshSlotFromList(i);
        }
    }

    public void RefreshHotbarFromList()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            RefreshHotbarSlotFromList(i);
        }
    }

    public void RefreshSlotFromList(int index)
    {
        Slots[index]?.StoreItem(InventoryItems[index]);
    }

    public void RefreshHotbarSlotFromList(int index)
    {
        HotbarSlots[index]?.StoreItem(HotbarItems[index]);
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

    public Slot GetSlot(int index)
    {
        return Slots[index];
    }

    public void StoreItem(SlottedItem item)
    {
        int index = PutIntoUI(item);
        Slot slot = GetSlot(index);
        slot.StoreItem(item);

    }
}

    