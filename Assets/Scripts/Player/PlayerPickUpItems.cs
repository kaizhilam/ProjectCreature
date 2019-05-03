using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpItems : MonoBehaviour
{
    private GameObject selectedObj;
    private SlottedItem selectedItem;
    private bool isChecked = false;

	private void Update()
	{
		PickUpOperation();
		ViewInventory();
	}

	private void PickUpOperation()
	{
		//PRESS E
		if (Input.GetKeyDown(KeyCode.E))
		{
			//SET selectedObj TO OBJECT THAT THE CROSSHAIR IS LOOKING AT
			selectedObj = ThirdPersonCamera.LookingAtGameObject;
			//CHECK selectedObj tag
			if (selectedObj.tag == "T1")
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

	private void ViewInventory()
	{
		if (Input.GetKeyDown(KeyCode.B))
		{
            print("b pressed");
			InventoryManager.Instance.DisplaySwitch();
		}
		/*
		if (Input.GetKeyDown(KeyCode.N))
		{
			Knapsack.SetActive(false);
			//Knapsack.GetComponent<CanvasRenderer>() = false;
		}
		*/
	}

	//   private void MouseOperation()
	//   {
	//       if (Input.GetMouseButton(1))
	//       {
	//           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	//           RaycastHit hitInfo;

	//           if (Physics.Raycast(ray, out hitInfo))
	//           {
	//               selectedObj = hitInfo.collider.gameObject;
	//               SelectItem();
	//           }
	//       }

	//   }

	//   private void SelectItem()
	//   {
	//       if (selectedObj.CompareTag("T1"))
	//       {
	//           selectedItem = (Item)selectedObj.GetComponent<Item>();
	//           if (selectedItem.IsCloseEnough() == true)
	//           {
	//               Debug.Log("The collectable item " + selectedItem.objName + "has been selected but not be added in your pack");
	//               isChecked = true;
	//           }
	//       }
	//   }

	//private void PickUpOperation()
	//{
	//	if (Input.GetKeyDown(KeyCode.E) && isChecked == true && selectedItem.IsCloseEnough() == true)
	//	{
	//		Inventory.Add(selectedItem);
	//		Knapsack.Instance.StoreItem(selectedItem);
	//		Debug.Log("The item has been added");
	//		isChecked = false;
	//		selectedObj.SetActive(false);
	//	}
	//}
}
