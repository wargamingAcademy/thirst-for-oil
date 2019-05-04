using Gamekit2D;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// класс хранящий основную информацию об уровне
/// </summary>
public class LevelManager : MonoBehaviour
{
    public AvailibleBuildingData availibleBuildingTilemap;
    public ResourceData resourceTilemap;
    public GridTilemap gridTilemap;
    public Tilemap fogeOfWarTilemap;
    public BuildingData buildingTilemap;

    /// <summary>
    /// ширина клетки тайлмапа в еденицах unity
    /// </summary>
    public static float WidthCell { get; set; }

    public Vector2Int worldSize;
    public Vector2Int offset;

    public GeneralBuilding buildings;


    /// <summary>
    /// Список всех зданий в палитре
    /// </summary>
    public List<TileBase> tiles;

    /// <summary>
    /// иконки всех зданий
    /// </summary>
    public Sprite[] sprites;

    /// <summary>
    /// палитра зданий
    /// </summary>
    private GameObject buildingsPrefab;

    // Start is called before the first frame update
    void Start()
    {
        fogeOfWarTilemap = GameObject.Find(ObjectnamesConstant.FOGE_OF_WAR_TILEMAP).GetComponent<Tilemap>();
        availibleBuildingTilemap.Initialize(worldSize);
        resourceTilemap.Initialize();
        gridTilemap.Initialize(worldSize,offset);
        buildingTilemap.Initialize();
        GameObject go=GameObject.Find("BuildingTilemap");

        PlayerInput.Instance.EnableButtons();

        tiles = new List<TileBase>();
        buildingsPrefab = Resources.Load<GameObject>(PathConstants.PATH_PALETTES + PathConstants.BUILDINGS);
        sprites = Resources.LoadAll<Sprite>(PathConstants.PATH_SPRITES+PathConstants.BUILDINGS);
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
    /// <param name="name">имя тайла</param>
    /// <returns></returns>
    public Tile GetTile(string name)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.name == name)
            {
                return tile;
            }
        }
        throw new FileNotFoundException("Спрайт не найден");
    }

    /// <summary>
    /// Ищем спрайт по имени
    /// </summary>
    /// <param name="name">имя спрайта</param>
    /// <returns></returns>
    public Sprite GetSprite(string name)
    {
        foreach (Sprite sprite in sprites)
        {
            if (sprite.name == name)
            {
                return sprite;
            }
        }
        return null;
    }
}
