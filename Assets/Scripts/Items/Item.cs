using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemScriptableObject _Base;

    public string Name { get; private set; }
    public string Description { get; private set; }
    public Mesh Mesh { get; private set; }
    public Material Material { get; private set; }

    private void Start()
    {
        Name = _Base.Name;
        Description = _Base.Description;
        Mesh = _Base.Mesh;
        Material = _Base.Material;

        MeshCollider mesh = gameObject.AddComponent<MeshCollider>();
        mesh.sharedMesh = Mesh;
        mesh.convex = true;
        gameObject.AddComponent<MeshRenderer>();
        gameObject.AddComponent<Rigidbody>();
    }

    private void Update()
    {
        Graphics.DrawMesh(Mesh, transform.position, transform.rotation, Material, 0);
    }
}
