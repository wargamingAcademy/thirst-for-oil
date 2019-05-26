using Assets.Scripts.Constants;
using Assets.Scripts.Modificators.BuildingsModificators.PriceBuildingModificators;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OilRig : GeneralBuilding
{

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
        var modificator = (OilRigIncomeModificator)ModificatorManager.Instance.GetResourceModificator(new OilRigIncomeModificator());
        return "Добывает ["+ modificator.GetOilIncome(Prices.AMOUNT_OIL_PRODUCING) + "] ед. нефти в ход.";
    }

    public override bool IsCanBeBuild(Vector2Int position)
    {   
        
        if (!(levelManager.resourceData.Resources.GetLength(0) > position.x - levelManager.offset.x) ||
            !(levelManager.resourceData.Resources.GetLength(1) > position.y - levelManager.offset.y))
        {
            return false;        
        }
        if (levelManager.resourceData.Resources[position.x-levelManager.offset.x, position.y - levelManager.offset.y] == Resource.Oil)
        {
            if (levelManager.availibleBuildingData.IsAvailibleBuilding[position.x - levelManager.offset.x, position.y - levelManager.offset.y] == true)
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
        resourceManager.IncomeOil +=modificator.GetOilIncome(Prices.AMOUNT_OIL_PRODUCING); 
    }

    public override void OnEndTurn()
    {
        throw new System.NotImplementedException();
    }

    public override bool[,] GetAvailibleCells()
    {
        bool[,] result=new bool[levelManager.availibleBuildingData.IsAvailibleBuilding.GetLength(0), levelManager.availibleBuildingData.IsAvailibleBuilding.GetLength(1)];
        for (int i = 0; i < levelManager.availibleBuildingData.IsAvailibleBuilding.GetLength(0); i++)
        {
            for (int j=0;j<levelManager.availibleBuildingData.IsAvailibleBuilding.GetLength(1);j++)
            {
                if ((levelManager.resourceData.Resources[i, j] == Resource.Oil)&&levelManager.availibleBuildingData.IsAvailibleBuilding[i,j])
                {
                    result[i, j] = true;
                }
                else
                {
                    result[i, j] = false;
                }
            }
        }
        return result;
    }

    public override string GetName()
    {
        return NamesBuildingRus.OIL_RIG_NAME;
    }
}