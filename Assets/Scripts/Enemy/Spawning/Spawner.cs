using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Spawner : MonoBehaviour
{
    public int spawnCap;
    public float Range;
    public abstract void SpawnEnemy();


}
