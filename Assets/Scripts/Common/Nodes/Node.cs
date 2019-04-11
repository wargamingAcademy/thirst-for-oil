using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///Реализация узла дерева 
/// </summary>
public abstract class Node
{
    private static int currentIndex = 0;
    public static int GetNextIndex
        {
        get {
            return currentIndex++;
        }
        }
    public Vector2Int Coordinate { get; set; }
    public Vector2Int Size { get; set; }
    public bool ISGreenZone { get; set; }

    /// <summary>
    /// номер узла
    /// </summary>
    private int id;

    /// <summary>
    /// Какие узлы предпочтительнее выбирать при построении пути
    /// </summary>
    public int PathPriority { get; set; }

    /// <summary>
    /// координаты клеток на карте
    /// </summary>
   // private Vector2Int[,] coordinates;

    /// <summary>
    /// Как далеко находится узел от корневого узла
    /// </summary>
    private int depth;

    /// <summary>
    /// Входит ли узел в итоговое решение
    /// </summary>
    bool includedInSolution = false;

    public Node()
    {
        id = 0;
    }

    /* public Node(Vector2Int coordinate, Vector2Int size, int parDepth,int wholeSize,int jumpDownSize,int jumpUpSize)
     {
         id = GetNextIndex;
         this.Coordinate = coordinate;
         this.Size = size;
         depth = parDepth;
         WholeSize = wholeSize;
         JumpDownDepth = jumpDownSize;
         JumpUpDepth = jumpUpSize;
     }*/
    public Node(Vector2Int coordinate, Vector2Int size, int parDepth)
   {
       id = GetNextIndex;
       this.Coordinate = coordinate;
       this.Size = size;
       depth = parDepth;
   }
    public Node(Vector2Int coordinate,Vector2Int size, int parDepth,bool isGreenZone)
    {
        id = GetNextIndex;
        this.Coordinate = coordinate;
        this.Size = size;
        depth = parDepth;
        ISGreenZone = isGreenZone;
      //  ISWhole = isWhole;
    }

    /// <summary>
    /// Номер узла
    /// </summary>
    public int Id
    {
        get { return id; }
    }

    public int Depth
    {
        set { depth = value; }
        get { return depth; }
    }
}