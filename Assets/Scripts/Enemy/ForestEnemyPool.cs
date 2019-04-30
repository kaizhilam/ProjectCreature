using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestEnemyPool : MonoBehaviour
{
    [SerializeField] private List<ForestEnemy> forestEnemyPrefabs;
    private Queue<ForestEnemy> ForestEnemies = new Queue<ForestEnemy>();
    public static ForestEnemyPool Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    
    public ForestEnemy Get()
    {
        if(ForestEnemies.Count == 0)
        {
            AddEnemies(1);
        }
        return ForestEnemies.Dequeue();
    }

    private void AddEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int RandomIndex = UnityEngine.Random.Range(0, forestEnemyPrefabs.Count); 
            ForestEnemy enemy = Instantiate(forestEnemyPrefabs[RandomIndex]);
            enemy.gameObject.SetActive(false);
            ForestEnemies.Enqueue(enemy); 
        }
    }

    public void ReturnToPool(ForestEnemy en)
    {
        en.gameObject.SetActive(false);
        ForestEnemies.Enqueue(en);
    }
}
