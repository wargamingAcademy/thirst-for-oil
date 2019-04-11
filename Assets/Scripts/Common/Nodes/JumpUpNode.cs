using UnityEngine;
using UnityEditor;

public class JumpUpNode : Node
{
    public JumpUpNode(Vector2Int coordinate, Vector2Int size, int parDepth, int jumpUpDepth) :
      base(coordinate, size, parDepth)
    {
        JumpUpDepth = jumpUpDepth;
        PathPriority = 5;
    }
    public JumpUpNode(Vector2Int coordinate, Vector2Int size, int parDepth, int jumpUpDepth,bool isGreenZone) :
    base(coordinate, size, parDepth,isGreenZone)
    {
        JumpUpDepth = jumpUpDepth;
        PathPriority = 5;
    }
    public int JumpUpDepth { get; set; }
}