using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "AvailibleBuildingTilemap", menuName = "AvailibleBuildingTilemap", order = 55)]
public class AvailibleBuildingTilemap : ScriptableObject
{
    public bool[,] IsAvailibleBuilding;
    private Tilemap tilemap;

    public void Initialize(Vector2Int worldSize)
    {
        GameObject tilemapGameObject = GameObject.Find(ObjectnamesConstant.AVAILIBLE_BUILDING);
        tilemap=tilemapGameObject.GetComponent<Tilemap>();
        if (tilemap == null)
        {
            throw new ArgumentNullException();
        }
        BoundsInt bounds = tilemap.cellBounds;
        IsAvailibleBuilding = new bool[worldSize.x,worldSize.y];
        for (int x = 0; x < worldSize.x; x++)
        {
            for (int y = 0; y < worldSize.y; y++)
            {
                IsAvailibleBuilding[x, y] = true;
            }
        }

        for (int x = 0; x < tilemap.size.x; x++)
        {
            for (int y = 0; y < tilemap.size.y; y++)
            {
                Vector2Int size = new Vector2Int();
                size.x = tilemap.size.x % 2 > 0 ? tilemap.size.x / 2 + 1 : tilemap.size.x / 2;
                size.y = tilemap.size.y % 2 > 0 ? tilemap.size.y / 2 + 1 : tilemap.size.y / 2 + 1;

                TileBase tileBase = tilemap.GetTile(new Vector3Int(x + bounds.position.x, y + bounds.position.y, 0));
                if (tileBase == null)
                {
                    continue;
                }
                switch (tileBase.name)
                {
                    case "noBuilding":
                        IsAvailibleBuilding[x, y] = false; break;
                    case "building":
                        IsAvailibleBuilding[x, y] = true; break;
                }
            }
        }
    }
}