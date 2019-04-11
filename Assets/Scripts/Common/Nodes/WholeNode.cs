using UnityEngine;
using UnityEditor;

public class WholeNode : Node
{
   public WholeNode(Vector2Int coordinate, Vector2Int size, int parDepth,int wholeSize) :
       base(coordinate, size, parDepth)
    {
        WholeSize = wholeSize;
        PathPriority = 5;
    }

    public int WholeSize { get; set; }
}