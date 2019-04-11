using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System;

[CreateAssetMenu(fileName = "ResourceTilemapData", menuName = "ResourceTilemapData", order = 54)]
public class ResourceTilemapData : ScriptableObject
{
    public Resource[,] Resources { get; set; }
    public GameObject tilemapGameobject;
    private Tilemap tilemap;
    public void Initialize()
    {
        tilemap = tilemapGameobject.GetComponent<Tilemap>();
        if (tilemap == null)
        {
            throw new ArgumentNullException();
        }
        BoundsInt bounds = tilemap.cellBounds;
        Resources = new Resource[tilemap.size.x, tilemap.size.y];
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
                    case "oil":
                        Resources[x, y] = Resource.Oil; break;                  
                }
            }
        }
    }
}