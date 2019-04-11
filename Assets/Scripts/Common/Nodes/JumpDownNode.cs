using UnityEngine;
using UnityEditor;

public class JumpDownNode : Node
{
    public JumpDownNode(Vector2Int coordinate, Vector2Int size, int parDepth, int jumpDownDepth) :
      base(coordinate, size, parDepth)
    {
        JumpDownDepth = jumpDownDepth;
        PathPriority = 5;
    }
    public JumpDownNode(Vector2Int coordinate, Vector2Int size, int parDepth, int jumpDownDepth,bool isGreenZone) :
     base(coordinate, size, parDepth,isGreenZone)
    {
        JumpDownDepth = jumpDownDepth;
        PathPriority = 5;
    }
    public int JumpDownDepth { get; set; }
}