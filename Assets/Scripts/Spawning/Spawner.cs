using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Spawner : MonoBehaviour
{
    public int spawnCap = 1;
    private List<Enemy> enemies = new List<Enemy>();
    public float maxSpawnAngle;
    public Enemy f1;
    private Vector3 spawnOffset;    
    // Start is called before the first frame update
    void Start()
    {
        maxSpawnAngle = 30;
        SpawnEnemy();
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    void SpawnEnemy()
    {
        //how each enemy spawn location is determined
        //imagining a 2D circle on the xz plane, pick a random point on that circle (random direction and random from 0->radius)
        //convert to 3d point by using polar to cartesian conversion (r and theta to x and z [y is that of the spawner])
        //ray cast from this point to the ground
        //wherever ray lands, spawn enemy at this location
        //place these spawners with enough space for it to draw the imagined 2D circle
        float direction;
        float radius;
        float radAngle = maxSpawnAngle * Mathf.PI / 180;
        for (int i = 0; i < 20; i++)
        {
            //have more functionality. if(player in certain biome) {spawn specific enemy}
            //right now its just creating firstEnemies
            direction = Random.Range(0,2 * Mathf.PI);
            radius = Random.Range(0, (Mathf.PI/2)*180-radAngle);

            enemies.Add(Instantiate(
                f1,
                new Vector3(this.transform.position.x + Random.Range(-10f, 10f), this.transform.position.y + Random.Range(-10f, 10f), this.transform.position.z + Random.Range(-10f, 10f)),
                Quaternion.identity
            ));
            
            //enemies.Add((Enemy)f1.Clone());
        }
    }


}
