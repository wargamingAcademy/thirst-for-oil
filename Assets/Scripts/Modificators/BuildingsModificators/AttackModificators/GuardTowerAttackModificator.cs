using Assets.Scripts.Modificators.BuildingsModificators.AttackModificators;

namespace Assets.Scripts.Modificators.AttackModificators
{
    public class GuardTowerAttackModificator : BaseAttackModificator
    {
        public float AttackPowerInPercentDieselDogs { get; set; }

        public override float GetAttackPower(float amount)
        {
            return amount * AttackPowerInPercentDieselDogs;
        }

        public override GeneralBuilding GetBuilding()
        {
            throw new System.NotImplementedException();
        }

        public override void Initialize()
        {
            AttackPowerInPercentDieselDogs = 1f;
        }
    }
}
