using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knapsack : MonoBehaviour
{
    private float targetAlpha = 1;

    private float smoothing = 4;

    private CanvasGroup canvasGroup;

    protected Slot[] slotList;

    public virtual void Start()
    {
        slotList = GetComponentsInChildren<Slot>();
        canvasGroup = GetComponent<CanvasGroup>();
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

    private static Knapsack _instance;
    public static Knapsack Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("KnapsackPanel").GetComponent<Knapsack>();
            }
            return _instance;
        }
    }


    public void StoreItem(Item item)
    {
        Slot slot = FindEmptySlot();
        slot.StoreItem(item);

    }

    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
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
}
