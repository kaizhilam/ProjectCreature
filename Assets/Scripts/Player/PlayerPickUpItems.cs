using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpItems : MonoBehaviour
{
    private GameObject selectedObj;
    private SlottedItem selectedItem;

    private void Start()
    {
        GetComponent<InputManager>().EKey += PickUpOperation;
    }


    private void PickUpOperation()
    {
        if(Physics.Raycast(ThirdPersonCamera.castRay, out RaycastHit hit, 11.0f, 1<<LayerMask.NameToLayer("item")))
        {

            //we use the cameras ray but with a mask so it will only detect gameObjects with weapon layer (in future should be item layer)
            selectedObj = hit.collider.gameObject;
        }
        //CHECK selectedObj tag
        if (selectedObj != null && selectedObj.tag == "T1")
        {
            //REST OF MyLi's CODE
            selectedItem = (SlottedItem)selectedObj.GetComponent<SlottedItem>();
            if (selectedItem.IsCloseEnough())
            {
                print("selected TIEM");

                //ADD TO INVENTORY
                InventoryManager.Instance.PutIntoUI(selectedItem);
                //InventoryManager.Instance.StoreItem(selectedItem);
                selectedObj.SetActive(false);
                selectedObj.tag = "Untagged";
                selectedObj.layer = 2;
            }
        }
        else
        {
            Debug.Log("Can't add this GameObject to Inventory");
        }
        
    }
}
