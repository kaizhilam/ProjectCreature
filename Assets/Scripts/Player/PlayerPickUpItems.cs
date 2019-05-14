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
            //we use the cameras ray but with a mask so it will only detect gameObjects with weapon layer (in future should be item layer)
            selectedObj = hit.collider.gameObject;
        }
        //CHECK selectedObj tag
        print(selectedObj);
            if (selectedObj!=null && selectedObj.tag == "T1")
            {
            //REST OF MyLi's CODE
            selectedItem = (SlottedItem)selectedObj.GetComponent<SlottedItem>();
            if (selectedItem.IsCloseEnough() == true)
            {
                isChecked = true;
                //ADD TO INVENTORY
                InventoryManager.Instance.PutIntoUI(selectedItem);
                //InventoryManager.Instance.StoreItem(selectedItem);
                isChecked = false;
                selectedObj.SetActive(false);
                selectedObj.tag = "Untagged";
            }
        }
            else
            {
                Debug.Log("Can't add this GameObject to Inventory");
            }
        
    }
}
