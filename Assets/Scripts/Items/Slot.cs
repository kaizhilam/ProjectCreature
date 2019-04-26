using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject itemPrefab;
    //private Image itemImage;
    //public Item item;


    /*
    private Image ItemImage
    {
        get
        {
            if (itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
            return itemImage;
        }
    }
    */

    public void StoreItem(Item item)
    {
        GameObject itemGameObject = Instantiate(itemPrefab) as GameObject;
        itemGameObject.transform.SetParent(this.transform);
        itemGameObject.transform.localScale = Vector3.one;
        itemGameObject.transform.localPosition = Vector3.zero;
        itemGameObject.GetComponent<ItemUI>().SetItem(item);
        //SetItem(item);
    }
    /*
    public void SetItem(Item item)
    {
        //transform.localScale = animationScale;
        this.item = item;
        //this.Amount = amount;
        // update ui 
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
    }
    */  

}
