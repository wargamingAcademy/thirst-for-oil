using UnityEngine;
using UnityEngine.Tilemaps;

public static class Construction
{
    public static AvailibleBuildingTilemap availibleBuilding;
    public static BuildingTilemap buildingTilemap;
    private static Tilemap fogeOfWarTilemap;
    static Construction()
    {
        availibleBuilding = Resources.Load<AvailibleBuildingTilemap>(PathConstants.SCRIPTABLE_OBJECTS + ObjectnamesConstant.AVAILIBLE_BUILDING);
        buildingTilemap= Resources.Load<BuildingTilemap>(PathConstants.SCRIPTABLE_OBJECTS + ObjectnamesConstant.BUILDING_TILEMAP);
        fogeOfWarTilemap = GameObject.Find(ObjectnamesConstant.FOGE_OF_WAR_TILEMAP).GetComponent<Tilemap>();

    }
    /// <summary>
    /// Построить здание
    /// </summary>
    /// <param name="coordinate">позиция строения</param>
    /// <param name="building">здание</param>
    /// <returns>true если успешно построили</returns>
    public static bool ConstructBuilding(Vector2Int coordinate,IBuilding building)
    {
        TileBase tile = fogeOfWarTilemap.GetTile(new Vector3Int(coordinate.x, coordinate.y, 0));
        if (tile == null)
        {
            return false;       
        }

       bool isFogeOfWar = tile.name==TileNames.FOGE;
        bool isNotAvailibleBuildBuilding = !availibleBuilding.IsAvailibleBuilding[coordinate.x, coordinate.y];
        if ((isNotAvailibleBuildBuilding) &&(isFogeOfWar))
        {
            return false;
        }
        availibleBuilding.IsAvailibleBuilding[coordinate.x, coordinate.y] = false;
        buildingTilemap.SetTile(new Vector3Int(coordinate.x,coordinate.y,0), building.GetTile());        
          return true;
    }

  /*  private static Tile GetTile(Building building)
    {
        switch (building)
        {
            
        }
    }*/
}