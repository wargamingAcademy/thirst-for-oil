using Gamekit2D;
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
        Tilemap t = go.GetComponent<Tilemap>();
        TileBase ta = t.GetTile(new Vector3Int(2, 2, 0));
        PlayerInput.Instance.EnableButtons();
    }
}
