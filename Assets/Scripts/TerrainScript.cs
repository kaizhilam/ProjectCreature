using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(Terrain.activeTerrain.collectDetailPatches);
        Terrain.activeTerrain.collectDetailPatches = false;
    }
    
}
