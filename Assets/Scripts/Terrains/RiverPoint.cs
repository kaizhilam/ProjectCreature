using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverPoint
{
    public float height;
    public int x;
    public int y;
    public float f;
    public float g;
    public float h;
    public RiverPoint cameFrom;

    public RiverPoint(float height, int x, int y)
    {
        this.height = height;
        this.x = x;
        this.y = y;
        this.f = 0;
        this.g = 0;
        this.h = 0;
    }
}
