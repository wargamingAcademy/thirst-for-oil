using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Modificators.BuildingsModificators.AttackModificators
{
    public abstract class BaseAttackModificator : BaseBuildingModificator, IAttackModificator
    {
        public abstract float GetAttackPower(float amount);
    }
}
