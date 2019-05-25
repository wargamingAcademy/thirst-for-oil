using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using Assets.Scripts.Modificators.BuildingsModificators.PriceBuildingModificators;
[Serializable]
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

    public override Sprite GetSpriteIcon()
    {
        return levelManager.GetIconSprite(TileNames.GUARD_TOWER_ICON);
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

        if (!(levelManager.availibleBuildingData.IsAvailibleBuilding.GetLength(0) > position.x) ||
            !(levelManager.availibleBuildingData.IsAvailibleBuilding.GetLength(1) > position.y))
        {
            return false;
        }
        if (levelManager.availibleBuildingData.IsAvailibleBuilding[position.x-levelManager.offset.x, position.y - levelManager.offset.y] == true)
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

    public override bool[,] GetAvailibleCells()
    {
        return levelManager.availibleBuildingData.IsAvailibleBuilding;
       // bool[,] result = new bool[levelManager.worldSize.x, levelManager.worldSize.y];

    }
}