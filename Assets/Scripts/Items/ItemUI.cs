using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        this.amount = amount;
        ItemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        if (item.capacity > 1)
            AmountText.text = amount.ToString();
        else
            AmountText.text = "";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
