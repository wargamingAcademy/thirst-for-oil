using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BuildingTilemap", menuName = "BuildingTilemap", order = 52)]
public class BuildingTilemap : ScriptableObject
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
        //  tilemap.RefreshAllTiles();
    }
}