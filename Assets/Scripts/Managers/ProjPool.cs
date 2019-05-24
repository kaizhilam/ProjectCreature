using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjPool : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    private Queue<Projectile> Projectiles = new Queue<Projectile>();
    public static ProjPool Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public Projectile Get()
    {
        if (Projectiles.Count == 0)
        {
            AddProjectiles(1);
        }
        return Projectiles.Dequeue();
    }

    private void AddProjectiles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Projectile enemy = Instantiate(projectilePrefab);
            enemy.gameObject.SetActive(false);
            Projectiles.Enqueue(enemy);
        }
    }

    public void ReturnToPool(Projectile en)
    {
        en.gameObject.SetActive(false);
        Projectiles.Enqueue(en);
    }
}