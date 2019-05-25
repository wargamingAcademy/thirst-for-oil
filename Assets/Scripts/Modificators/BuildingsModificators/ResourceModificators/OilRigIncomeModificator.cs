using Assets.Scripts.Modificators;

public class OilRigIncomeModificator : BaseBuildingModificator
{
    public float PercentageProductionDieselDogs{get; set;}
    public float PercentageProductionOldPipe { get; set; }
    public float PercentageProductionWeAllGonnaDie { get; set; }
    public float GetOilIncome(float amount)
    {
        return amount*PercentageProductionDieselDogs* PercentageProductionOldPipe * PercentageProductionWeAllGonnaDie;
    }

    public override GeneralBuilding GetBuilding()
    {
        return new OilRig();
    }

    public override void Initialize()
    {
        PercentageProductionDieselDogs = 1f;
        PercentageProductionOldPipe = 1f;
        PercentageProductionWeAllGonnaDie = 1f;
    }
}