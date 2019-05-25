using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using Assets.Scripts.Modificators.BuildingsModificators.PriceBuildingModificators;

public class GuardTower : GeneralBuilding
{
    public override TileBase GetTile()
    {
        return levelManager.GetTile(TileNames.GUARD_TOWER);
    }
    public override Sprite GetSprite()
    {
        return levelManager.GetSprite(TileNames.GUARD_TOWER);
    }

    public override float GetPrice()
    {
        ModificatorManager.Instance.RegisterResourceModificator(new PriceBuildingModificator());
        var modificator = (PriceBuildingModificator)ModificatorManager.Instance.GetResourceModificator(new PriceBuildingModificator());
        return modificator.GetPrice(Prices.GUARD_TOWER_PRICE);
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
        
    }

    public override void OnEndTurn()
    {
        throw new System.NotImplementedException();
    }
}