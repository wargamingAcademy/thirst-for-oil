using UnityEngine;
using UnityEngine.Tilemaps;
using System;

/// <summary>
/// хранит информацию о ресурсах на карте
/// </summary>
[CreateAssetMenu(fileName = "ResourceData", menuName = "ResourceData", order = 54)]
public class ResourceData : ScriptableObject
{
    public Resource[,] Resources { get; set; }
    public Tilemap tilemap;

    public void Initialize()
    {
        GameObject tilemapGameObject = GameObject.Find(ObjectnamesConstant.RESOURCE_TILEMAP);
        tilemap = tilemapGameObject.GetComponent<Tilemap>();
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
    public void SetTile()
    { }
}