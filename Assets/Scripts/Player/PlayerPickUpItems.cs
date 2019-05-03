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

            //SET selectedObj TO OBJECT THAT THE CROSSHAIR IS LOOKING AT
            selectedObj = ThirdPersonCamera.LookingAtGameObject;
            //CHECK selectedObj tag
            if (selectedObj!=null && selectedObj.tag == "T1")
            {
                //REST OF MyLi's CODE
                selectedItem = (SlottedItem)selectedObj.GetComponent<SlottedItem>();
                if (selectedItem.IsCloseEnough() == true)
                {
                    Debug.Log("The collectable item " + selectedItem.objName + "has been selected but not be added in your pack");
                    isChecked = true;
                    //ADD TO INVENTORY
                    int indexOfInsertedItem = InventoryManager.Instance.AddToFirstInvSlot(selectedItem);
                    InventoryManager.Instance.RefreshSlotFromList(indexOfInsertedItem);
                    InventoryManager.Instance.StoreItem(selectedItem);

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
