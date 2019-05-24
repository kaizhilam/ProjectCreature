using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSpawner : Spawner
{
    private Vector3 spawnOffset;
    public List<GameObject> forest = new List<GameObject>();    
    private Ray spawnRay;

    public override void SpawnEnemy(int numberToSpawn)
    {

        //how each enemy spawn location is determined
        //imagining a 2D circle on the xz plane around the spawner object, pick a random point on that circle (random theta and random f from 0->radius)
        //convert to 3d point by using polar to cartesian conversion (r and theta to x and z [y is that of the spawner])
        //ray cast from this point straight down to the ground
        //wherever ray lands, spawn enemy at this location
        //Ensure spawner is placed above where you want enemies to potentially spawn, try not to have imaginary circle intersect object in scene
        //!!if the ray never hits land, it will spawn enemy at spawner location!! (better solution to be added)

        //converting degrees to radians
        for (int i = 0; i < numberToSpawn; i++)
        {
            Vector3 TrySpawnLocation;
            int tries = 0;
            //will keep trying to find a valid spawnLocation
            //if it can't after 100 tries, uses the latest invalid location
            do
            {
                tries++;
                TrySpawnLocation = GetRandomPointInCircle();
            } while (!isValid(TrySpawnLocation) && tries<100);

            spawnRay = new Ray(TrySpawnLocation, -Vector3.up);

            if (Physics.Raycast(spawnRay, out RaycastHit hit))
            {
                TrySpawnLocation = hit.point + Vector3.up * 5;

            }


            ForestEnemy inst = ForestEnemyPool.Instance.Get();
            inst.transform.rotation = Quaternion.identity;
            inst.transform.position = TrySpawnLocation;
            inst.gameObject.SetActive(true);
            enemies.Add(inst);
        }

        
    }

    public Vector3 GetRandomPointInCircle()
    {
        float direction;
        float radius;
        //have more functionality. if(spawner in certain biome) {spawn specific enemy}
        //right now its just creating firstEnemies
        direction = Random.Range(0, 2 * Mathf.PI);
        radius = Random.Range(0, Range);
        float x = radius * Mathf.Cos(direction);
        float z = radius * Mathf.Sin(direction);
        Vector3 castDownPoint = new Vector3(x + this.transform.position.x, this.transform.position.y, z + this.transform.position.z);
        Vector3 downVector = new Vector3(0, -1, 0);
        
        return castDownPoint;
    }

    public bool isValid(Vector3 castDownPoint)
    {
        //if casts down do the ground, returns true
        if (Physics.Raycast(spawnRay, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("obstacle")))
        {
            if(hit.collider.gameObject.tag == "Ground")
            {
                return true;
            }
            print("invalid point");
            return false;
        }
        return false;
    }
}
