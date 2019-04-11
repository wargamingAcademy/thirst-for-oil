using UnityEngine;
using UnityEngine.Tilemaps;

public class MainBase : GeneralBuilding,IBuilding
{
    public void Initialize()
    {
        //var textFile = Resources.Load<GameObject>(BuildingNames.PATH_BUILDING + BuildingNames.MAIN_BASE);
    }
    public TileBase GetTile()
    {
       return GetTile(TileNames.MAIN_BASE);
    }
}