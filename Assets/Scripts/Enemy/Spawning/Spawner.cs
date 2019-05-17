using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Spawner : MonoBehaviour
{
    public int spawnCap;
    public float Range;
    public abstract void SpawnEnemy(int numberToSpawn);
    protected List<Enemy> enemies = new List<Enemy>();


    private void Start()
    {
        SpawnEnemy(spawnCap);
        StartCoroutine(Respawning());

    }

    private void LateUpdate()
    {
        enemies.RemoveAll(e => (e.gameObject.activeInHierarchy == false));
    }

    IEnumerator Respawning()
    {

        while (true)
        {
            print(enemies.Count + " " + spawnCap);
            if (enemies.Count < spawnCap)
            {
                print("respawning");
                SpawnEnemy(spawnCap - enemies.Count);
                print(enemies.Count);
            }
            yield return new WaitForSeconds(5);
        }
    }

}
