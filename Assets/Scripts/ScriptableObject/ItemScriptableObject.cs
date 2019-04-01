using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemScriptableObject : ScriptableObject
{
    public string Name;
    public string Description;
    public Mesh Mesh;
    public Material Material;
}
