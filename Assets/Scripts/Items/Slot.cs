using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject itemPrefab;

    public void StoreItem(SlottedItem item)
    {
        GameObject itemGameObject = Instantiate(itemPrefab) as GameObject;
        itemGameObject.transform.SetParent(this.transform);
        itemGameObject.transform.localScale = Vector3.one;
        itemGameObject.transform.localPosition = Vector3.zero;
        itemGameObject.GetComponent<ItemUI>().SetItem(item);
        //SetItem(item);
    }


}
