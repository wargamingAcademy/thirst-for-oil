using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine.Tilemaps;

/// <summary>
/// Хранит общую информацию для зданий
/// </summary>
[CreateAssetMenu(fileName = "GeneralBuilding", menuName = "GeneralBuilding", order = 57)]
public class GeneralBuilding:ScriptableObject
{
    /// <summary>
    /// палитра зданий
    /// </summary>
    private GameObject buildingsPrefab;


    /// <summary>
    /// Список всех зданий в палитре
    /// </summary>
    public List<TileBase> tiles;
    public void OnEnable()
    {
        tiles=new List<TileBase>();
        buildingsPrefab = Resources.Load<GameObject>(PathConstants.PATH_PALETTES + PathConstants.BUILDINGS);
        var tilemap = buildingsPrefab.GetComponentInChildren<Tilemap>();
        for (int x = 0; x < tilemap.size.x; x++)
        {
            for (int y = 0; y < tilemap.size.y; y++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    tiles.Add(tile);
                }
            }
        }

    }

    /// <summary>
    /// Ищем тайл по имени
    /// </summary>
    /// <param name="name">имя спрайта</param>
    /// <returns></returns>
    public Tile  GetTile(string name)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.name == name)
            {
                return tile;
            }
        }
        return null;
    }
}