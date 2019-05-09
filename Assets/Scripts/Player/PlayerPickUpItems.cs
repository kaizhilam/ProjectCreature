using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpItems : MonoBehaviour
{
    private GameObject selectedObj;
    private SlottedItem selectedItem;
    private bool isChecked = false;

    private void Start()
    {
        GetComponent<InputManager>().EKey += PickUpOperation;
    }


    private void PickUpOperation()
    {
        if(Physics.Raycast(ThirdPersonCamera.castRay, out RaycastHit hit, LayerMask.NameToLayer("item")))
        {
            print("found weapon");
            //we use the cameras ray but with a mask so it will only detect gameObjects with weapon layer (in future should be item layer)
            selectedObj = hit.collider.gameObject;
        }
            print(selectedObj);
            //CHECK selectedObj tag
            if (selectedObj!=null && selectedObj.tag == "T1")
            {
            //REST OF MyLi's CODE
            print("1");
            selectedItem = (SlottedItem)selectedObj.GetComponent<SlottedItem>();
            print("2");
            print(selectedItem.name);
            print(selectedItem.IsCloseEnough());
            if (selectedItem.IsCloseEnough() == true)
            {
                isChecked = true;
                //ADD TO INVENTORY
                InventoryManager.Instance.PutIntoUI(selectedItem);
                //InventoryManager.Instance.StoreItem(selectedItem);
                Debug.Log("The item has been added");
                isChecked = false;
                selectedObj.SetActive(false);
            }
        }
            else
            {
                Debug.Log("Can't add this GameObject to Inventory");
            }
        
    }
}
