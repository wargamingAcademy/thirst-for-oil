using UnityEngine;
using UnityEditor;

public class NormalNode :Node
{
   public NormalNode(Vector2Int coordinate, Vector2Int size, int parDepth) :
        base(coordinate,size,parDepth)
    {
        PathPriority = 3;
    }
    public NormalNode(Vector2Int coordinate, Vector2Int size, int parDepth,bool isGreenZone) :
       base(coordinate, size, parDepth,isGreenZone)
    {
        PathPriority = 3;
    }
}