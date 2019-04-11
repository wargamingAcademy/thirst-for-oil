using UnityEngine;

public class StairNode : Node
{
    public StairNode(Vector2Int coordinate, Vector2Int size, int parDepth) :
        base(coordinate, size, parDepth)
    {
        PathPriority = 1;
    }
    public StairNode(Vector2Int coordinate, Vector2Int size, int parDepth, bool isGreenZone) :
       base(coordinate, size, parDepth, isGreenZone)
    {
        PathPriority = 1;
    }
}