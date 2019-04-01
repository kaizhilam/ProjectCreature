using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ProjectileScriptableObject _Base;

    public string Name { get; private set; }
    public string Description { get; private set; }
    public float Damage { get; private set; }
    public float TimeActive { get; private set; }
    public float Speed { get; private set; }
    public float RechargeTimer { get; private set; }
    public Mesh Mesh { get; private set; }
    public Material Material { get; private set; }

    private void Start()
    {
        _Base = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>().ProjectileAbility;
        ThirdPersonCamera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ThirdPersonCamera>();
        if (camera == null)
        {
            Destroy(gameObject);
        }
        Name = _Base.Name;
        Description = _Base.Description;
        Damage = _Base.Damage;
        TimeActive = _Base.TimeActive;
        Speed = _Base.Speed;
        RechargeTimer = _Base.RechargeTimer;
        Mesh = _Base.Mesh;
        Material = _Base.Material;

        transform.LookAt(camera.LookingAtPoint);

        MeshCollider mesh = gameObject.AddComponent<MeshCollider>();
        mesh.sharedMesh = Mesh;
        gameObject.AddComponent<MeshRenderer>();
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.angularDrag = 0;
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
    }

    void Update()
    {
        Graphics.DrawMesh(Mesh, transform.position, transform.rotation, Material, 0);
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        TimeActive -= Time.deltaTime;
        if (TimeActive <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
