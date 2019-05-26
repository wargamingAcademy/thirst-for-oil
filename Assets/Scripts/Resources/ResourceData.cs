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
    private LevelManager levelManager;
    public void Initialize()
    {
        levelManager = FindObjectOfType<LevelManager>();
        var tilemapGameObject = GameObject.Find(ObjectnamesConstant.RESOURCE_TILEMAP);
        tilemap = tilemapGameObject.GetComponent<Tilemap>();
        if (tilemap == null)
        {
            throw new ArgumentNullException();
        }
        BoundsInt bounds = tilemap.cellBounds;
        Resources = new Resource[levelManager.availibleBuildingData.IsAvailibleBuilding.GetLength(0), levelManager.availibleBuildingData.IsAvailibleBuilding.GetLength(1)];
        for (int x = 0; x < Resources.GetLength(0); x++)
        {
            for (int y = 0; y < Resources.GetLength(1); y++)
            {
                TileBase tileBase = tilemap.GetTile(new Vector3Int(x + levelManager.offset.x, y + levelManager.offset.y, 0));
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