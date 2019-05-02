using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpItems : MonoBehaviour
{
    private PlayerInventory<Item> Inventory = new PlayerInventory<Item>();
    private GameObject selectedObj;
    private Item selectedItem;
    private bool isChecked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseOperation();
        PickUpOperation();
        ViewInventory();
    }

    private void ViewInventory()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log(Inventory);
            Knapsack.Instance.DisplaySwitch();
        }
        /*
        if (Input.GetKeyDown(KeyCode.N))
        {
            Knapsack.SetActive(false);
            //Knapsack.GetComponent<CanvasRenderer>() = false;
        }
        */
    }

    private void MouseOperation()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                selectedObj = hitInfo.collider.gameObject;
                SelectItem();
            }
        }

    }

    private void SelectItem()
    {
        if (selectedObj.CompareTag("T1"))
        {
            selectedItem = (Item)selectedObj.GetComponent<Item>();
            if (selectedItem.IsCloseEnough() == true)
            {
                Debug.Log("The collectable item " + selectedItem.objName + "has been selected but not be added in your pack");
                isChecked = true;
            }
        }
    }

    private void PickUpOperation()
    {
        if (Input.GetKeyDown(KeyCode.E) && isChecked == true && selectedItem.IsCloseEnough() == true)
        {
            Inventory.Add(selectedItem);
            Knapsack.Instance.StoreItem(selectedItem);
            Debug.Log("The item has been added");
            isChecked = false;
            selectedObj.SetActive(false);
        }
    }
}
