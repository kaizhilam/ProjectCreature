using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager: MonoBehaviour
{
    private float targetAlpha = 1;
    private float smoothing = 4;
    private CanvasGroup canvasGroup;
    private CanvasGroup HotbarCanvasGroup;
    private CanvasGroup InventoryCanvasGroup;
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
        //Bind b to the DisplaySwitch function
        GameObject.Find("Player").GetComponent<InputManager>().BKey += DisplaySwitch;
        //get all slot object
        Slots = GetComponentsInChildren<Panel>()[0].GetComponentsInChildren<Slot>();
        HotbarSlots = gameObject.transform.parent.GetComponentInChildren<Hotbar>().GetComponentsInChildren<Slot>();
        //grab both canvases and parent canvas 
        canvasGroup = GetComponent<CanvasGroup>();
        HotbarCanvasGroup = gameObject.transform.parent.GetComponentInChildren<Hotbar>().GetComponent<CanvasGroup>();
        InventoryCanvasGroup = canvasGroup.gameObject.GetComponentInChildren<CanvasGroup>();
        //Creating an array that will mirror the hotbar and inv contents
        InventoryItems = new SlottedItem[Slots.Length];
        HotbarItems = new SlottedItem[HotbarSlots.Length];
        FastHideInv();
    }

    //Swaps two slots contents
    public void Swap(Slot slot1, Slot slot2)
    {
        SlottedItem item1 = GetItemFromSlot(slot1);
        SlottedItem item2 = GetItemFromSlot(slot2);
        int index1 = GetIndexOfSlot(slot1);
        int index2 = GetIndexOfSlot(slot2);
        bool isInHotbar1 = IsInHotbar(slot1);
        bool isInHotbar2 = IsInHotbar(slot2);
        print("from " + isInHotbar1 + ", to " + isInHotbar2);
        //Depending on which panel the slots are in, we target different panels slots
        //if swapping hotbar items for example, we are only concerned with hotbar slots
        if(!isInHotbar1 && !isInHotbar2)
        {
            print("inv to inv swap");
            int invIndex1 = index1;
            int invIndex2 = index2;
            SlottedItem temp = InventoryItems[invIndex1];
            InventoryItems[invIndex1] = InventoryItems[invIndex2];
            InventoryItems[invIndex2] = temp;
            RefreshSlotFromList(invIndex1);
            RefreshSlotFromList(invIndex2);
        }
        else if(isInHotbar1 && !isInHotbar2)
        {
            print("bar to inv swap");
            int hotbarindex1 = index1;
            int invIndex1 = index2;
            SlottedItem temp = HotbarItems[hotbarindex1];
            HotbarItems[hotbarindex1] = InventoryItems[invIndex1];
            InventoryItems[invIndex1] = temp;
            RefreshHotbarSlotFromList(hotbarindex1);
            RefreshSlotFromList(invIndex1);
        }
        else if(!isInHotbar1 && isInHotbar2)
        {
            print("inv to bar swap");
            int invIndex1 = index1;
            int hotbarindex1 = index2;
            SlottedItem temp = InventoryItems[invIndex1];
            InventoryItems[invIndex1] = HotbarItems[hotbarindex1];
            HotbarItems[hotbarindex1] = temp;
            RefreshSlotFromList(invIndex1);
            RefreshHotbarSlotFromList(hotbarindex1);
        }
        else //if both in hotbar
        {
            print("bar to bar swap");
            int hotbarindex1 = GetIndexOfSlot(slot1);
            int hotbarindex2 = GetIndexOfSlot(slot2);
            SlottedItem temp = HotbarItems[hotbarindex1];
            HotbarItems[hotbarindex1] = HotbarItems[hotbarindex2];
            HotbarItems[hotbarindex2] = temp;
            RefreshHotbarSlotFromList(hotbarindex1);
            RefreshHotbarSlotFromList(hotbarindex2);
        }
    }

    //update UI slots from arrays
    private void RefreshAll()
    {
        RefreshInventoryFromList();
        RefreshHotbarFromList();
    }

    //not used yet, used for player throwing away items
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

    //returns index of first slot in inv that is free, returns -1 if none
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

    //return index of first slot in hotbar that is free, returns -1 if none
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
        //check if item in hotbar already
        if (indexHotbar > -1)
        {
            List<int> indexes = GetHotbarIndexesOfItem(item);
            //if can hold it, ++
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
            for (int i = 0; i < indexes.Count; i++)
            {
                if (InventoryItems[indexes[i]].count < InventoryItems[indexes[i]].capacity)
                {
                    InventoryItems[indexes[i]].count++;
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

    //get item ojbect given a slot object
    public SlottedItem GetItemFromSlot(Slot slot)
    {
        return slot.GetComponentInChildren<ItemUI>()?.item;
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

    //finds index of item in the inventory
    public int GetIndex(SlottedItem item)
    {
        for (int i = 0; i < InventoryItems.Length; i++)
        {
            if(item?.objID == InventoryItems[i]?.objID)
            {
                return i;
            }
        }
        return -1;
    }

    //given a slot, will find the index in inv
    public int GetIndexOfSlot(Slot slot)
    {
        return slot.transform.GetSiblingIndex();
    }

    //returns index of item in hotbar, -1 if there isn't any
    public int GetFirstHotbarIndexOfItem(SlottedItem item)
    {
        if(item == null)
        {
            return -1;
        }
        for (int i = 0; i < HotbarItems.Length; i++)
        {
            if(item.objID == HotbarItems[i]?.objID)
            {
                return i;
            }
        }
        return -1;
    }

    //Returns list of all indexes of item in hotbar
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

    //Returns list of all indexes of item in inventory
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

    //returns bool, is item in hotbar?
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

    //returns bool, is slot in hotbar? (or in inv?)
    public bool IsInHotbar(Slot slot)
    {
        for (int i = 0; i < HotbarSlots.Length; i++)
        {
            if(HotbarSlots[i].GetInstanceID() == slot.GetInstanceID())
            {
                return true;
            }
        }
        return false;
    }

    //given index, get item from inv
    public SlottedItem GetItem(int index)
    {
        return InventoryItems[index];
    }

    //given index, get item from hotbar
    public SlottedItem GetHotbarItem(int index)
    {
        return HotbarItems[index];
    }

    //Refresh Inventory from inventory array
    public void RefreshInventoryFromList()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            RefreshSlotFromList(i);
        }
    }

    //Refresh Hotbar from hotbar array
    public void RefreshHotbarFromList()
    {
        for (int i = 0; i < HotbarSlots.Length; i++)
        {
            RefreshHotbarSlotFromList(i);
        }
    }
    
    //will update the inv slot from array using index 
    public void RefreshSlotFromList(int index)
    {
        Slots[index].StoreItem(InventoryItems[index]);
    }

    //will update the hotbar slot from array using index
    public void RefreshHotbarSlotFromList(int index)
    {
        HotbarSlots[index].StoreItem(HotbarItems[index]);
    }

    //toggles visibility of inventory panel
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

    //don't lerp, set inv hidden straight away
    public void FastHideInv()
    {
        targetAlpha = 0;
        InventoryCanvasGroup.alpha = 0;
    }

    //will cause update function to slowly bring back visibility to inv panel
    public void Show()
    {
        InventoryCanvasGroup.blocksRaycasts = true;
        targetAlpha = 1;
        CursorManager.Visible = true;
        InputManager.EnabledPlayInput = false;
    }

    //will cause update function to slowly bring back visibility to inv panel
    public void Hide()
    {
        InventoryCanvasGroup.blocksRaycasts = false;
        targetAlpha = 0;
        CursorManager.Visible = false;
        InputManager.EnabledPlayInput = true;
    }

    //inv panel will gradually reach targetAlpha if not there already
    private void Update()
    {
        //if (HotbarCanvasGroup.alpha != targetAlpha)
        //{
        //    HotbarCanvasGroup.alpha = Mathf.Lerp(HotbarCanvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
        //    if (Mathf.Abs(HotbarCanvasGroup.alpha - targetAlpha) < .01f)
        //    {
        //        HotbarCanvasGroup.alpha = targetAlpha;
        //    }
        //}
        if (InventoryCanvasGroup.alpha != targetAlpha)
        {
            InventoryCanvasGroup.alpha = Mathf.Lerp(InventoryCanvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if (Mathf.Abs(InventoryCanvasGroup.alpha - targetAlpha) < .01f)
            {
                InventoryCanvasGroup.alpha = targetAlpha;
            }
        }
    }

    //given index, get slot object
    public Slot GetSlot(int index)
    {
        return Slots[index];
    }

}

    