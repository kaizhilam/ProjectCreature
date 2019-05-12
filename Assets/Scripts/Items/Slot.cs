using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject itemPrefab;
    GameObject item;
    GameObject oldParent;
    private bool dragging = false;
    public delegate void SwitchDelegate();

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging)
        {
            item = GetComponentInChildren<ItemUI>()?.gameObject;
        }
        if (item != null)
        {
            dragging = true;
            
        }
        if (dragging)
        {
            dragging = true;
            item.transform.position = Input.mousePosition;
            item.transform.SetParent(GameObject.Find("UI").transform);
        }
        else
        {
            print("touching nothing");
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            pointerId = -1
        };

        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        bool found = false;
        foreach (var hit in results)
        {
            //if found somewhere to move the item to, move it there
            if (hit.gameObject.GetComponent<Slot>() != null)
            {
                Destroy(item);
                //item.transform.SetParent(hit.gameObject.transform);
                //item.transform.localPosition = Vector3.zero;
                found = true;
                item = null;
                dragging = false;
                print("swapping " + this.name + " " + hit.gameObject.name);
                InventoryManager.Instance.Swap(this, hit.gameObject.GetComponent<Slot>());
                break;
            }

        }
        if (!found)
        {
            print("no spot found for this item");
            item.transform.SetParent(this.transform);
            item.transform.localPosition = Vector3.zero;
            dragging = false;
            item = null;
        }
       
    }

    public void StoreItem(SlottedItem item)
    {
        if (item == null)
        {
            Destroy(GetComponentInChildren<ItemUI>()?.gameObject);
        }
        else //wanting to add something
        {
            if (GetComponentInChildren<ItemUI>() == null)
            {
                GameObject itemGameObject = Instantiate(itemPrefab) as GameObject;
                itemGameObject.transform.SetParent(this.transform);
                itemGameObject.transform.localScale = Vector3.one;
                itemGameObject.transform.localPosition = Vector3.zero;
            }
            GetComponentInChildren<ItemUI>().SetItem(item);
            //SetItem(item);
        }


    }

    public void HighlightSlot()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Backgrounds/button_square_h");
    }

    public void UnhighlightSlot()
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Backgrounds/button_square");
    }


}
