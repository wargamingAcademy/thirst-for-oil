namespace Assets.Scripts.Modificators
{
    public abstract class BaseBuildingModificator : IModificator,IBuildingModificator
    {
        public abstract GeneralBuilding GetBuilding();

        public abstract void Initialize();
    }
}
