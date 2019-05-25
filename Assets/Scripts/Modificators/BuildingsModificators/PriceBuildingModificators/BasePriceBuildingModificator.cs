namespace Assets.Scripts.Modificators.BuildingsModificators.PriceBuildingModificators
{
    public abstract class BasePriceBuildingModificator : IModificator, IPriceBuildingModificator
    {
        public abstract float GetPrice(float startPrice);
        public abstract void Initialize();
    }
}
