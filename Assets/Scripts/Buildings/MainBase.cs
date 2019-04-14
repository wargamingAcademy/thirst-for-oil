using UnityEngine;
using UnityEngine.Tilemaps;

public class MainBase : GeneralBuilding
{
    public void Initialize()
    {
        //var textFile = Resources.Load<GameObject>(BuildingNames.PATH_BUILDING + BuildingNames.MAIN_BASE);
    }
    public override TileBase GetTile()
    {
       return GetTile(TileNames.MAIN_BASE);
    }
}