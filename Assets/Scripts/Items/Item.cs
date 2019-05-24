using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
public class Item
{
    public string ObjectSlug { get; set; }
    public string Description { get; set; }
    public string ItemTypes { get; set; }
    public string ActionName { get; set; }
    public string ItemName { get; set; }
    

    public bool ItemModifier { get; set; }


    [Newtonsoft.Json.JsonConstructor]
    public Item(string _ObjectSlug, string _ItemName, string _Description, string _ItemType, string _ActionName, bool _ItemModifier)
    {
        this.ObjectSlug = _ObjectSlug;
        this.Description = _Description;
        this.ActionName = _ActionName;
        this.ItemName = _ItemName;
        this.ItemModifier = _ItemModifier;
    }
}
