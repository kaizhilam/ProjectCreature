using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour
{
    public SlottedItem item { get; set; }
    public int amount { get; set; }
    private Image itemImage;
    private Text amountText;

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
    private Text AmountText
    {
        get
        {
            if (amountText == null)
            {
                amountText = GetComponentInChildren<Text>();
            }
            return amountText;
        }
    }

    public void SetItem(SlottedItem item)
    {
        this.item = item;
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        //if (item.count > 1)
            AmountText.text = item.count.ToString();
        //else
            //AmountText.text = "1";
    }
    // Start is called before the first frame update
    void Start()
    {
        amount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnDrop(PointerEventData eventData)
    {
        this.transform.localPosition = Vector3.zero;
    }
}
