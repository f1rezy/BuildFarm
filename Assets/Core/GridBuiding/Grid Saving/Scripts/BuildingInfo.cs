using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfo
{
    public readonly int X;
    public readonly int Y;

    public readonly int XSize;
    public readonly int YSize;

    public readonly string Type;

    public BuildingInfo(int x, int y, int xSize, int ySize, string type)
    {
        X = x;
        Y = y;
        XSize = xSize;
        YSize = ySize;
        Type = type;
    }
}
