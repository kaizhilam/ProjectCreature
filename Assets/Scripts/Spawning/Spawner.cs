using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int spawnCap = 20;
    private List<Enemy> enemies = new List<Enemy>();
    
    private Enemy Enemy;
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
        FirstEnemy spawningEnemy = new FirstEnemy((EnemyScriptable)ScriptableObject.CreateInstance("EnemyScriptable"), this.transform.position);
        Enemy = spawningEnemy;
        for (int i = 0; i < 20; i++)
        {
            //have more functionality. if(player in certain biome) {spawn specific enemy}
            //right now its just creating firstEnemies
            enemies.Add((Enemy)Instantiate(
                Enemy,
                transform.position,
                Quaternion.identity
            ));
            //enemies.Add((Enemy)f1.Clone());
        }
    }


}
