using System;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// хранит информацию о построенных зданиях
/// </summary>
[CreateAssetMenu(fileName = "BuildingData", menuName = "BuildingData", order = 52)]
public class BuildingData : ScriptableObject
{
    private Tilemap tilemap;
    public void Initialize()
    {
        GameObject tilemapGameObject = GameObject.Find(ObjectnamesConstant.BUILDING_TILEMAP);
        tilemap = tilemapGameObject.GetComponent<Tilemap>();
        //tilemap.ClearAllTiles();
    }

    public Building[,] Buildings
    {
        get
        {
            if (Buildings == null)
            {
                if (tilemap == null)
                {
                    throw new ArgumentNullException("BuildingTilemap is null");
                }
                Buildings = new Building[tilemap.size.x, tilemap.size.x];
            }
            return Buildings;
        }
        set { Buildings = value; }
    }

    public void SetTile(Vector3Int position,TileBase tile)
    {
        tilemap.SetTile(position,tile);
    }
}