using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrainHeights : MonoBehaviour
{
    // Start is called before the first frame update
    public int depth = 100;
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
        //heights = AddRiver(heights);
        return heights;
    }
    public float[,] AddRiver(float[,] heights)
    {
        RiverPoint[] startEnd = GetRiverStartEnd(heights);
        List<RiverPoint> path = getRiverPath(startEnd);
        Debug.Log("startX: " + startEnd[0].x + " startY: " + startEnd[0].y + " endX: " + startEnd[1].x + " endY: " + startEnd[1].y + " heightStart: " + startEnd[0].height + " heightEnd: " + startEnd[1].height);
        heights[startEnd[0].x, startEnd[0].y] = 0;
        heights[startEnd[1].x, startEnd[1].y] = 0;
        return heights;
    }

    private List<RiverPoint> getRiverPath(RiverPoint[] startEnd)
    {
        //big wip
        //need to use a* pathfinding algorithm to generate path using heights as heuristics as opposed to distance to goal.
        List<RiverPoint> openSet = new List<RiverPoint>();
        openSet.Add(startEnd[0]);
        throw new NotImplementedException();
    }

    public RiverPoint[] GetRiverStartEnd(float[,] heights)
    {
        //a pairing of the start and end point to be returned by this function
        //only works if terrain is square
        RiverPoint[] startEnd = new RiverPoint[2];
        List<RiverPoint> edgePoints = new List<RiverPoint>();
        //loop through all edge points in terrain
        for (int i = 0; i < heights.GetLength(0); i++)
        {
            edgePoints.Add(new RiverPoint(heights[i, 0], i, 0));
            edgePoints.Add(new RiverPoint(heights[i, heights.GetLength(0) - 1], i, heights.GetLength(0) - 1));
            if (i != 0 && i != heights.GetLength(0) - 1)
            {
                edgePoints.Add(new RiverPoint(heights[0, i], 0, i));
                edgePoints.Add(new RiverPoint(heights[heights.GetLength(0) - 1, i], heights.GetLength(0) - 1, i));
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
        else if (startPoint.x == heights.GetLength(0) - 1)
        {
            edgeNum = 2;
        }
        if (startPoint.y == 0)
        {
            edgeNum = 1;
        }
        else if (startPoint.y == heights.GetLength(1) - 1)
        {
            edgeNum = 3;
        }

        int oppositeEdge = (edgeNum + 2) % 4;
        List<RiverPoint> oppositeEdges = new List<RiverPoint>();
        //make array for opposite edges heights
        switch (oppositeEdge)
        {
            case 2:
                for (int i = 0; i < heights.GetLength(0); i++)
                {
                    oppositeEdges.Add(new RiverPoint(heights[heights.GetLength(0)-1, i], heights.GetLength(0) - 1, i));
                    Debug.Log("case 2");
                }
                break;
            case 3:
                for (int i = 0; i < heights.GetLength(0); i++)
                {
                    Debug.Log("case 3");
                    oppositeEdges.Add(new RiverPoint(heights[i, heights.GetLength(0) - 1], i, heights.GetLength(0) - 1));
                }
                break;
            case 0:
                for (int i = 0; i < heights.GetLength(0); i++)
                {
                    Debug.Log("case 0");
                    oppositeEdges.Add(new RiverPoint(heights[0, i], 0, i));
                }
                break;
            case 1:
                
                for (int i = 0; i < heights.GetLength(0); i++)
                {
                    Debug.Log("Case 1");
                    oppositeEdges.Add(new RiverPoint(heights[i, 0], i, 0));
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
