using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareClass : IComparer<RiverPoint>
{
    public int Compare(RiverPoint p1, RiverPoint p2)
    {
        float h1 = p1.height;
        float h2 = p2.height;

        return h2.CompareTo(h1);

    }
}
