using Assets.Scripts.Modificators.BuildingsModificators.PriceBuildingModificators;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OilRig : GeneralBuilding
{
    public const float AMOUNT_OIL_PRODUCING = 10; 

    public override TileBase GetTile()
    {
       return levelManager.GetTile(TileNames.OIL_RIG);
    }

    public override Sprite GetSprite()
    {
        return levelManager.GetSprite(TileNames.OIL_RIG);
    }

    public override Sprite GetSpriteIcon()
    {
        return levelManager.GetIconSprite(TileNames.OIL_RIG_ICON);
    }

    public override float GetPrice()
    {
        ModificatorManager.Instance.RegisterResourceModificator(new PriceBuildingModificator());
        var modificator = (PriceBuildingModificator)ModificatorManager.Instance.GetResourceModificator(new PriceBuildingModificator());
        return modificator.GetPrice(Prices.OIL_RIG_IN_OIL_PRICE);
    }

    public override string GetDescription()
    {
        return "Добывает "+AMOUNT_OIL_PRODUCING+"ед. нефти в ход";
    }

    public override bool IsCanBeBuild(Vector2Int position)
    {   
        
        if (!(levelManager.resourceData.Resources.GetLength(0) > position.x) ||
            !(levelManager.resourceData.Resources.GetLength(1) > position.y))
        {
            return false;        
        }
        if (levelManager.resourceData.Resources[position.x, position.y] == Resource.Oil)
        {
            if (levelManager.availibleBuildingData.IsAvailibleBuilding[position.x, position.y] == true)
            {
                return true;
            }
        }
        return false;
    }

    public override void OnBuilding()
    {
        ModificatorManager.Instance.RegisterResourceModificator(new OilRigIncomeModificator());
        var modificator =(OilRigIncomeModificator) ModificatorManager.Instance.GetResourceModificator(new OilRigIncomeModificator());
        resourceManager.IncomeOil +=modificator.GetOilIncome(AMOUNT_OIL_PRODUCING); 
    }

    public override void OnEndTurn()
    {
        throw new System.NotImplementedException();
    }

    public override bool[,] GetAvailibleCells()
    {
        throw new System.NotImplementedException();
    }
}