﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public int objID;
    public string objName;
    public string description;
    public GameObject player;
    private float distance;
    private bool isCloseEnough;
    public ItemType Type;
    public string Sprite;
    public int capacity;

    public Item()
    {

    }

    public Item(int id, string name, string des, ItemType type, int capacity)
    {
        objID = id;
        objName = name;
        description = des;
        this.Type = type;
        this.capacity = capacity;
    }

    private void Update()
    {
        JudgeDistance();
    }

    private void JudgeDistance()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 5)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            isCloseEnough = true;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
            isCloseEnough = false;
        }
    }

    public bool IsCloseEnough()
    {
        return isCloseEnough;
    }

    public enum ItemType
    {
        Consumable,
        Equipment,
        Weapon
        //Material
    }
    //public ItemScriptableObject _Base;

    //public string Name { get; private set; }
    //public string Description { get; private set; }
    //public Mesh Mesh { get; private set; }
    //public Material Material { get; private set; }

    //private void Start()
    //{
    //    Name = _Base.Name;
    //    Description = _Base.Description;
    //    Mesh = _Base.Mesh;
    //    Material = _Base.Material;

    //    MeshCollider mesh = gameObject.AddComponent<MeshCollider>();
    //    mesh.sharedMesh = Mesh;
    //    mesh.convex = true;
    //    gameObject.AddComponent<MeshRenderer>();
    //    gameObject.AddComponent<Rigidbody>();
    //}

    //private void Update()
    //{
    //    Graphics.DrawMesh(Mesh, transform.position, transform.rotation, Material, 0);
    //}
}
