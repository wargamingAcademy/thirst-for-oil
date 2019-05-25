namespace Assets.Scripts.Modificators.BuildingsModificators.PriceBuildingModificators
{
    public class PriceBuildingModificator : BasePriceBuildingModificator
    {
        public float MysteriousTunnelsPriceCoeff { get; set; }
        public float WeHaveLossesPriceCoeff { get; set; }
        public float WeAllGonnaDiePriceCoeff { get; set; }
        public override float GetPrice(float startPrice)
        {
            return startPrice * MysteriousTunnelsPriceCoeff* WeHaveLossesPriceCoeff* WeAllGonnaDiePriceCoeff;
        }

        public override void Initialize()
        {
            MysteriousTunnelsPriceCoeff = 1f;
            WeHaveLossesPriceCoeff = 1f;
            WeAllGonnaDiePriceCoeff = 1f;
        }
    }
}
