using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrainHeights : MonoBehaviour
{
    // Start is called before the first frame update
    public int depth;
    public int width = 256;
    public int height = 256;
    public float scale = 1f;
    public CompareClass sorter = new CompareClass();

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }
    // Update is called once per frame
    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / (float)width * scale;
                float yCoord = (float)y / (float)height * scale;
                float xCoord2 = (float)x / (float)width * (scale * 2);
                float yCoord2 = (float)y / (float)height * (scale * 2);
                float xCoord3 = (float)x / (float)width * (scale * 4);
                float yCoord3 = (float)y / (float)height * (scale * 4);
                float xCoord4 = (float)x / (float)width * (scale * 8);
                float yCoord4 = (float)y / (float)height * (scale * 8);

                heights[x, y] = (8.0f / 15.0f) * Mathf.PerlinNoise(xCoord, yCoord) +
                   (4.0f / 15.0f) * Mathf.PerlinNoise(xCoord2 + 1000, yCoord2 + 1000) +
                    (2.0f / 15.0f) * Mathf.PerlinNoise(xCoord3 + 2000, yCoord3 + 2000)
                    + (1.0f / 15.0f) * Mathf.PerlinNoise(xCoord4 + 3000, yCoord4 + 3000);
            }
        }
        heights = AddRiver(heights);
        return heights;
    }
    public float[,] AddRiver(float[,] heights)
    {
        Debug.Log("Addriver()");
        RiverPoint[,] objPoints = new RiverPoint[heights.GetLength(0), heights.GetLength(1)];
        for (int i = 0; i < heights.GetLength(0); i++)
        {
            for (int j = 0; j < heights.GetLength(1); j++)
            {
                objPoints[i, j] = new RiverPoint(heights[i, j], i, j);
            }
        }
        RiverPoint[] startEnd = GetRiverStartEnd(objPoints);
        List<RiverPoint> path = getRiverPath(startEnd, heights);
        Debug.Log("startX: " + startEnd[0].x + " startY: " + startEnd[0].y + " endX: " + startEnd[1].x + " endY: " + startEnd[1].y + " heightStart: " + startEnd[0].height + " heightEnd: " + startEnd[1].height);
        foreach (RiverPoint r in path)
        {
            Debug.Log(r.x + " " + r.y); 
            heights[r.x, r.y] = 0;

        }
        return heights;
    }

    private List<RiverPoint> getRiverPath(RiverPoint[] startEnd, float[,] heights)
    {
        //need to use a* pathfinding algorithm to generate path using heights as heuristics as well as distance to goal.
        List<RiverPoint> openSet = new List<RiverPoint>();
        List<RiverPoint> closedSet = new List<RiverPoint>();
        List<RiverPoint> path = new List<RiverPoint>();
        

        openSet.Add(startEnd[0]);
        int winner = 0;
        startEnd[0].f = Heuristic_cost_estimate(startEnd[0], startEnd[1]);
        //while still points to evaluate
        while (openSet.Count > 0)
        {
            Debug.Log(openSet.Count);
            // set current to openSet riverpoint with lowest f value
            for (int i = 0; i < openSet.Count; i++)
            {
                if (openSet[i].f < openSet[winner].f)
                {
                    winner = i;
                }
            }
            RiverPoint current = openSet[winner];
            //if river finished, construct the path
            if (current == startEnd[1] || openSet.Count>7)
            {
                Debug.Log("Path COmpleted!!!!");
                path = reconstruct_Path(current);
                //openSet.Clear();
            }
            //set current as evaluated and put in closedSet
            openSet.Remove(current);
            closedSet.Add(current);

            //add all neighbours to the openSet
            List<RiverPoint> neighbours = getNeighbours(current, heights);
            foreach (RiverPoint r in neighbours)
            {
                //if not in closed set, add to closed set
                //Debug.Log("neightbor addewd");

                if (closedSet.IndexOf(r) < 0)
                {
                    
                    //this 1 needs changing if moving diagonally
                    float tempG = current.g + 1;

                    //if r not in open set, add to open set
                    if (openSet.IndexOf(r) < 0)
                    {
                        Debug.Log("not in openset");
                        openSet.Add(r);
                    }
                    else
                    {
                        Debug.Log("already in openset??");
                    }
                    //if better path is found, update camefrom, g and find new f
                    if (tempG < r.g)
                    {
                        r.cameFrom = current;
                        r.g = tempG;
                        r.f = r.g + Heuristic_cost_estimate(r, startEnd[1]);
                    }
                    
                }

            
            }
            
        }
        return path;
    }

    private List<RiverPoint> reconstruct_Path(RiverPoint current)
    {
        List<RiverPoint> path = new List<RiverPoint>();
        while (current.cameFrom != null)
        {
            current = current.cameFrom;
            path.Add(current);
        }
        return path;
    }

    private List<RiverPoint> getNeighbours(RiverPoint current, float[,] heights)
    {
        List<RiverPoint> neighbours = new List<RiverPoint>();
        if (!(current.x - 1 < 0))
        {
            neighbours.Add(new RiverPoint(heights[current.x - 1, current.y], current.x - 1, current.y));
        }
        if (!(current.x + 1 >= heights.GetLength(0)))
        {
            neighbours.Add(new RiverPoint(heights[current.x + 1, current.y], current.x + 1, current.y));
        }
        if (!(current.y - 1 < 0))
        {
            neighbours.Add(new RiverPoint(heights[current.x, current.y - 1], current.x, current.y - 1));
        }
        if (!(current.y + 1 >= heights.GetLength(1)))
        {
            neighbours.Add(new RiverPoint(heights[current.x, current.y + 1], current.x, current.y + 1));
        }
        return neighbours;
    }

    private float Heuristic_cost_estimate(RiverPoint r, RiverPoint end)
    {
        //cost will be euclidean distance + height difference from previous
        //if height greater than past height, add penalty
        double x = end.x - r.x;
        double y = end.y - r.y;
        float h = Mathf.Sqrt((float)(x*x + y*y));
        if(r.cameFrom != null)
        h += r.height - r.cameFrom.height;

        return h;
    }

    public RiverPoint[] GetRiverStartEnd(RiverPoint[,] objPoints)
    {
        //a pairing of the start and end point to be returned by this function
        //only works if terrain is square
        RiverPoint[] startEnd = new RiverPoint[2];
        List<RiverPoint> edgePoints = new List<RiverPoint>();
        //loop through all edge points in terrain
        for (int i = 0; i < objPoints.GetLength(0); i++)
        {
            //edgePoints.Add(new RiverPoint(heights[i, 0], i, 0));
            edgePoints.Add(objPoints[i,0]);
            //edgePoints.Add(new RiverPoint(heights[i, heights.GetLength(0) - 1], i, heights.GetLength(0) - 1));
            edgePoints.Add(objPoints[i, objPoints.GetLength(0) - 1]);
            if (i != 0 && i != objPoints.GetLength(0) - 1)
            {
                edgePoints.Add(objPoints[0, i]);
                edgePoints.Add(objPoints[objPoints.GetLength(0) - 1, i]);
            }
        }
        //make them into riverPoints and add to array list - new RiverPoint(height,xCoord,  yCoord)
        //sort array by height
        edgePoints.Sort(sorter);

        //pick random from highest 3/5th
        RiverPoint startPoint = edgePoints[(int)Mathf.Floor(UnityEngine.Random.Range(0, Mathf.Floor(edgePoints.Count * 3 / 5)))];
        startEnd[0] = startPoint;
        //determine edge of chosen point
        int edgeNum = 0;
        if (startPoint.x == 0)
        {
            edgeNum = 0;
        }
        else if (startPoint.x == objPoints.GetLength(0) - 1)
        {
            edgeNum = 2;
        }
        if (startPoint.y == 0)
        {
            edgeNum = 1;
        }
        else if (startPoint.y == objPoints.GetLength(1) - 1)
        {
            edgeNum = 3;
        }

        int oppositeEdge = (edgeNum + 2) % 4;
        List<RiverPoint> oppositeEdges = new List<RiverPoint>();
        //make array for opposite edges heights
        switch (oppositeEdge)
        {
            case 2:
                for (int i = 0; i < objPoints.GetLength(0); i++)
                {
                    oppositeEdges.Add(objPoints[objPoints.GetLength(0)-1, i]);
                }
                break;
            case 3:
                for (int i = 0; i < objPoints.GetLength(0); i++)
                {
                    oppositeEdges.Add(objPoints[i, objPoints.GetLength(0) - 1]);
                }
                break;
            case 0:
                for (int i = 0; i < objPoints.GetLength(0); i++)
                {
                    oppositeEdges.Add(objPoints[0, i]);
                }
                break;
            case 1:
                
                for (int i = 0; i < objPoints.GetLength(0); i++)
                {
                    oppositeEdges.Add(objPoints[i, 0]);
                }
                break;
                
        }
        oppositeEdges.Sort(sorter);
        //pick lowest 1/5th value at random for endpoint
        RiverPoint endPoint = oppositeEdges[(int)Mathf.Floor(UnityEngine.Random.Range(Mathf.Floor(oppositeEdges.Count * 4 / 5), oppositeEdges.Count - 1))];
        startEnd[1] = endPoint;
        return startEnd;
    }
}
