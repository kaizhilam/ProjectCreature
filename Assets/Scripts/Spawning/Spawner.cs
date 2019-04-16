using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spawner : MonoBehaviour
{
    public int spawnCap = 1;
    private List<GameObject> enemies = new List<GameObject>();
    public float Range;
    public GameObject f1;
    private Vector3 spawnOffset;    
    // Start is called before the first frame update
    void Start()
    {
       
        SpawnEnemy();
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    void SpawnEnemy()
    {
        //how each enemy spawn location is determined
        //imagining a 2D circle on the xz plane around the spawner object, pick a random point on that circle (random theta and random f from 0->radius)
        //convert to 3d point by using polar to cartesian conversion (r and theta to x and z [y is that of the spawner])
        //ray cast from this point straight down to the ground
        //wherever ray lands, spawn enemy at this location
        //Ensure spawner is placed above where you want enemies to potentially spawn, try not to have imaginary circle intersect object in scene
        //!!if the ray never hits land, it will spawn enemy at spawner location!! (better solution to be added)
        float direction;
        float radius;
        //converting degrees to radians
        for (int i = 0; i < spawnCap; i++)
        {
            //have more functionality. if(spawner in certain biome) {spawn specific enemy}
            //right now its just creating firstEnemies
            direction = Random.Range(0,2 * Mathf.PI);
            radius = Random.Range(0, Range);
            float x = radius * Mathf.Cos(direction);
            float z = radius * Mathf.Sin(direction);
            Vector3 castDownPoint = new Vector3(x + this.transform.position.x, this.transform.position.y, z +  this.transform.position.z);
            Debug.Log(castDownPoint.x + " " + castDownPoint.y + " " + castDownPoint.z);
            Vector3 downVector = new Vector3(0, -1, 0);
            Ray spawnRay = new Ray(castDownPoint, downVector);
            Debug.DrawRay(castDownPoint, downVector, Color.green);
            Vector3 SpawnPoint;
            if (Physics.Raycast(spawnRay, out RaycastHit hit))
            {
                SpawnPoint = hit.point + Vector3.up*10;
            }
            else
            {
                Debug.Log("no land underneath spawner to spawn enemies");
                SpawnPoint = this.transform.position;
            }

            
            enemies.Add(Instantiate(
                f1,
                SpawnPoint,
                Quaternion.identity
            ));
            
            //enemies.Add((Enemy)f1.Clone());
        }
    }


}
