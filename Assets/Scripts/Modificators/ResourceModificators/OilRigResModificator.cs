public class OilRigResModificator :IResourceModificator
{
    public float PercentageProduction{get; set;}

    public float GetOilIncome(float amount)
    {
        return amount*PercentageProduction;
    }

    public GeneralBuilding GetBuilding()
    {
        return new OilRig();
    }

    public void Initialize()
    {
        PercentageProduction = 1f;
    }
}