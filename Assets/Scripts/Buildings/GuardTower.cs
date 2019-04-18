using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class GuardTower : GeneralBuilding
{
    public void Initialize()
    {
        //var textFile = Resources.Load<GameObject>(BuildingNames.PATH_BUILDING + BuildingNames.MAIN_BASE);
    }
    public override TileBase GetTile()
    {
        return GetTile(TileNames.GUARD_TOWER);
    }
    public override Sprite GetSprite()
    {
        return GetSprite(TileNames.GUARD_TOWER);
    }

    public override float GetPrice()
    {
        return Prices.GUARD_TOWER;
    }

    public override string GetDescription()
    {
        return "Защищает вас от врагов";
    }

    public override bool IsCanBeBuild(Vector2Int position)
    {

        if (!(levelManager.resourceTilemap.Resources.GetLength(0) > position.x) ||
            !(levelManager.resourceTilemap.Resources.GetLength(1) > position.y))
        {
            return false;
        }
        if (levelManager.availibleBuildingTilemap.IsAvailibleBuilding[position.x, position.y] == true)
        {
            return true;
        }

        return false;
    }

    public override void OnBuilding()
    {
        uiController.ChangeOilBar(-Prices.GUARD_TOWER);
        uiController.ChangeOilChangeBar(resourceManager.expenseOil);     
        uiController.ShowPriceBuildingOnBar(Prices.GUARD_TOWER);
    }

    public override void OnEndTurn()
    {
        throw new System.NotImplementedException();
    }
}