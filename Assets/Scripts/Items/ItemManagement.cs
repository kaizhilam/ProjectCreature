using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagement : MonoBehaviour
{
    #region
    private static ItemManagement _instance;

    public static ItemManagement Instance
    {
        get
        {
            if (_instance == null)
            {                
                _instance = GameObject.Find("ItemManagement").GetComponent<ItemManagement>();
            }
            return _instance;
        }
    }
    #endregion

    private List<Item> itemList;

    void ParseItemJson()
    {
        itemList = new List<Item>();
        TextAsset itemText = Resources.Load<TextAsset>("Items");
        string itemsJson = itemText.text;
        JSONObject j = new JSONObject(itemsJson);

        foreach(JSONObject temp in j.list)
        {
            string typeStr = temp["type"].str;
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), typeStr);

            int id = (int)(temp["id"].n);
            string name = temp["name"].str;
            string description = temp["description"].str;

            Item item = null;
            switch (type)
            {
                case Item.ItemType.Consumable:
                    int hp = (int)(temp["hp"].n);
                    int mp = (int)(temp["mp"].n);
                    int capacity = 10;
                    item = new Consumable(id, name, description, type, capacity, hp, mp);
                    break;
                //case Item.ItemType.Material:
                    //break;
                default:
                    break;
            }
            itemList.Add(item);
            Debug.Log(item);
        }
    }
}
