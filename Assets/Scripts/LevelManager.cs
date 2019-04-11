using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public AvailibleBuildingTilemap availibleBuildingTilemap;
    public ResourceTilemapData resourceTilemap;
    public GridTilemap gridTilemap;
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
        availibleBuildingTilemap.Initialize(worldSize);
        resourceTilemap.Initialize();
        gridTilemap.Initialize(worldSize,offset);
       // buildings.Initialize();
        buildingTilemap.Initialize();
        //Construction.ConstructBuilding(new Vector2Int(1, 1), new MainBase());
        GameObject go=GameObject.Find("BuildingTilemap");
        Tilemap t = go.GetComponent<Tilemap>();
        TileBase ta = t.GetTile(new Vector3Int(2, 2, 0));
    }
}
