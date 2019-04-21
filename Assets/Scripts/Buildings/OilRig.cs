using UnityEngine;
using UnityEngine.Tilemaps;

public class OilRig : GeneralBuilding
{
    public const float AMOUNT_OIL_PRODUCING = 10; 
    public void Initialize()
    {
        //var textFile = Resources.Load<GameObject>(BuildingNames.PATH_BUILDING + BuildingNames.MAIN_BASE);
    }
    public override TileBase GetTile()
    {
       return GetTile(TileNames.OIL_RIG);
    }
    public override Sprite GetSprite()
    {
        return GetSprite(TileNames.OIL_RIG);
    }

    public override float GetPrice()
    {
        return Prices.OIL_RIG_IN_OIL_PRICE;
    }

    public override string GetDescription()
    {
        return "Добывает "+AMOUNT_OIL_PRODUCING+"ед. нефти в ход";
    }

    public override bool IsCanBeBuild(Vector2Int position)
    {   
        
        if (!(levelManager.resourceTilemap.Resources.GetLength(0) > position.x) ||
            !(levelManager.resourceTilemap.Resources.GetLength(1) > position.y))
        {
            return false;        
        }
        if (levelManager.resourceTilemap.Resources[position.x, position.y] == Resource.Oil)
        {
            if (levelManager.availibleBuildingTilemap.IsAvailibleBuilding[position.x, position.y] == true)
            {
                return true;
            }
        }
        return false;
    }

    public override void OnBuilding()
    {
        ModificatorManager.Instance.RegisterResourceModificator(new OilRigResModificator());
        var modificator =(OilRigResModificator) ModificatorManager.Instance.GetResourceModificator(new OilRigResModificator());
        resourceManager.IncomeOil +=modificator.GetOilIncome(AMOUNT_OIL_PRODUCING); 
       
      /*  uiController.SetOilBarValue(-Prices.OIL_RIG_IN_OIL_PRICE);
        uiController.ChangeOilChangeBar(resourceManager.incomeOil);
        uiController.ShowPriceBuildingOnBar(Prices.OIL_RIG_IN_OIL_PRICE);*/
    }

    public override void OnEndTurn()
    {
        throw new System.NotImplementedException();
    }
}