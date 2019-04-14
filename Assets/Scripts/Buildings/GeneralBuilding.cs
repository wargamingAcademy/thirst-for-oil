using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine.Tilemaps;

/// <summary>
/// Хранит общую информацию для зданий
/// </summary>
public abstract class GeneralBuilding
{
    /// <summary>
    /// палитра зданий
    /// </summary>
    private GameObject buildingsPrefab;

    private LevelManager levelManager;

    /// <summary>
    /// Список всех зданий в палитре
    /// </summary>
    public List<TileBase> tiles;

    public GeneralBuilding()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }
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

    /// <summary>
    /// Построить здание
    /// </summary>
    /// <param name="coordinate">позиция строения</param>
    /// <param name="building">здание</param>
    /// <returns>true если успешно построили</returns>
    public bool ConstructBuilding(Vector2Int coordinate)
    {
        TileBase tile = levelManager.fogeOfWarTilemap.GetTile(new Vector3Int(coordinate.x, coordinate.y, 0));
        if (tile == null)
        {
            return false;
        }

        bool isFogeOfWar = tile.name == TileNames.FOGE;
        bool isNotAvailibleBuildBuilding = !levelManager.availibleBuildingTilemap.IsAvailibleBuilding[coordinate.x, coordinate.y];
        if ((isNotAvailibleBuildBuilding) && (isFogeOfWar))
        {
            return false;
        }
        levelManager.availibleBuildingTilemap.IsAvailibleBuilding[coordinate.x, coordinate.y] = false;
        levelManager.buildingTilemap.SetTile(new Vector3Int(coordinate.x, coordinate.y, 0), this.GetTile());
        return true;
    }

    public abstract TileBase GetTile();

}