using Gamekit2D;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public AvailibleBuildingTilemap availibleBuildingTilemap;
    public ResourceTilemapData resourceTilemap;
    public GridTilemap gridTilemap;
    public Tilemap fogeOfWarTilemap;
    public BuildingTilemap buildingTilemap;

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
       // buildings.Initialize();
        buildingTilemap.Initialize();
        //Construction.ConstructBuilding(new Vector2Int(1, 1), new MainBase());
        GameObject go=GameObject.Find("BuildingTilemap");

        PlayerInput.Instance.EnableButtons();

        tiles = new List<TileBase>();
        buildingsPrefab = Resources.Load<GameObject>(PathConstants.PATH_PALETTES + PathConstants.BUILDINGS);
        sprites = Resources.LoadAll<Sprite>(PathConstants.PATH_SPRITES);
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
}
