using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PathDrawer", menuName = "PathDrawer", order = 53)]
public class PathDrawer : ScriptableObject
{
    public Tile horizontalLine;
    public Tile verticalLine;
    public Tile[] tournant;
    public Tile[] objectif;
    public Tile inaccesible;
    public Tile greenZone;
    public Tile yellowZone;
    public void DrawMask(Tilemap maskTilemap, Graph graph, Vector2Int position)
    {
        BoundsInt boundsGreenYellowMask = maskTilemap.cellBounds;
        foreach (Node node in graph.Nodes)
        {
            if (node.ISGreenZone)
            {
                if (node.Coordinate != position)
                {
                    maskTilemap.SetTile(new Vector3Int(+node.Coordinate.x + boundsGreenYellowMask.x, node.Coordinate.y + boundsGreenYellowMask.y, 0), greenZone);
                }
            }
            else
            {
                maskTilemap.SetTile(new Vector3Int(node.Coordinate.x + boundsGreenYellowMask.x, node.Coordinate.y + boundsGreenYellowMask.y, 0), yellowZone);
            }
        }
    }
    public void Drawpath(Tilemap pathTilemap, Vector2Int cursorPosition, List<Node> path)
    {
        pathTilemap.ClearAllTiles();
        BoundsInt boundsPath = pathTilemap.cellBounds;
        if (path == null)
        {

            pathTilemap.SetTile(new Vector3Int(cursorPosition.x + boundsPath.x, cursorPosition.y + boundsPath.y, 0), inaccesible);
            return;
        }
        Node lastNode = null;

        for (int i = 1; i < path.Count - 1; i++)
        {
            if (lastNode == null)
            {
                lastNode = path[i];
            }
            //горизонтальная линия
            if ((path[i].Coordinate.y == path[i + 1].Coordinate.y) && (path[i].Coordinate.y == path[i - 1].Coordinate.y))
            {
                pathTilemap.SetTile(new Vector3Int(path[i].Coordinate.x + boundsPath.x, path[i].Coordinate.y + boundsPath.y, 0), horizontalLine);
            }
            //вертикальная линия
            if ((path[i].Coordinate.x == path[i + 1].Coordinate.x) && (path[i].Coordinate.x == path[i - 1].Coordinate.x))
            {

                pathTilemap.SetTile(new Vector3Int(path[i].Coordinate.x + boundsPath.x, path[i].Coordinate.y + boundsPath.y, 0), verticalLine);
            }
            //поворот слева-вниз
            if (((path[i].Coordinate.x > path[i + 1].Coordinate.x) && (path[i].Coordinate.y > path[i - 1].Coordinate.y) && (path[i].Coordinate.y == path[i + 1].Coordinate.y)) ||
                ((path[i - 1].Coordinate.x < path[i].Coordinate.x) && (path[i].Coordinate.y > path[i + 1].Coordinate.y) && (path[i].Coordinate.y == path[i - 1].Coordinate.y)))
            {
                pathTilemap.SetTile(new Vector3Int(path[i].Coordinate.x + boundsPath.x, path[i].Coordinate.y + boundsPath.y, 0), tournant[0]);
            }
            //поворот слева-вверх
            if (((path[i].Coordinate.x > path[i + 1].Coordinate.x) && (path[i].Coordinate.y < path[i - 1].Coordinate.y) && (path[i].Coordinate.y == path[i + 1].Coordinate.y)) ||
                ((path[i - 1].Coordinate.x < path[i].Coordinate.x) && (path[i].Coordinate.y < path[i + 1].Coordinate.y) && (path[i].Coordinate.y == path[i - 1].Coordinate.y)))
            {
                pathTilemap.SetTile(new Vector3Int(path[i].Coordinate.x + boundsPath.x, path[i].Coordinate.y + boundsPath.y, 0), tournant[3]);
            }
            //поворот справа-вниз
            if (((path[i].Coordinate.x < path[i + 1].Coordinate.x) && (path[i].Coordinate.y > path[i - 1].Coordinate.y) && (path[i].Coordinate.y == path[i + 1].Coordinate.y)) ||
                ((path[i - 1].Coordinate.x > path[i].Coordinate.x) && (path[i].Coordinate.y > path[i + 1].Coordinate.y) && (path[i].Coordinate.y == path[i - 1].Coordinate.y)))
            {
                pathTilemap.SetTile(new Vector3Int(path[i].Coordinate.x + boundsPath.x, path[i].Coordinate.y + boundsPath.y, 0), tournant[1]);
            }
            //поворот справа-вверх
            if (((path[i].Coordinate.x < path[i + 1].Coordinate.x) && (path[i].Coordinate.y < path[i - 1].Coordinate.y) && (path[i].Coordinate.y == path[i + 1].Coordinate.y)) ||
                ((path[i - 1].Coordinate.x > path[i].Coordinate.x) && (path[i].Coordinate.y < path[i + 1].Coordinate.y) && (path[i].Coordinate.y == path[i - 1].Coordinate.y)))
            {
                pathTilemap.SetTile(new Vector3Int(path[i].Coordinate.x + boundsPath.x, path[i].Coordinate.y + boundsPath.y, 0), tournant[2]);
            }
        }
        if (path.Count < 2)
        {
            return;
        }
        //рисуем метку цели
        if (path[1].Coordinate.y == path[0].Coordinate.y)
        {
            if (path[1].Coordinate.x < path[0].Coordinate.x)
            {
                pathTilemap.SetTile(new Vector3Int(path[0].Coordinate.x + boundsPath.x, path[0].Coordinate.y + boundsPath.y, 0), objectif[0]);
            }
            else
            {
                pathTilemap.SetTile(new Vector3Int(path[0].Coordinate.x + boundsPath.x, path[0].Coordinate.y + boundsPath.y, 0), objectif[1]);
            }
        }
        if (path[1].Coordinate.x == path[0].Coordinate.x)
        {
            if (path[1].Coordinate.y < path[0].Coordinate.y)
            {
                pathTilemap.SetTile(new Vector3Int(path[0].Coordinate.x + boundsPath.x, path[0].Coordinate.y + boundsPath.y, 0), objectif[3]);
            }
            else
            {
                pathTilemap.SetTile(new Vector3Int(path[0].Coordinate.x + boundsPath.x, path[0].Coordinate.y + boundsPath.y, 0), objectif[2]);
            }
        }
    }
}