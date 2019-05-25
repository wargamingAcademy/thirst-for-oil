using UnityEngine;
using Tasks;
using Assets.Scripts.Modificators.AttackModificators;

public class DieselDogsTask : BasicTask
{
    float _oilPenalty;
    float _attackPenalty;

    public override string GetTaskDescription()
    {
        string baseDescription = base.GetTaskDescription();
        switch (_progress)
        {
            case 1:
                return string.Format(baseDescription, Mathf.RoundToInt(_oilPenalty * 100f));
            case 2:
                return string.Format(baseDescription, Mathf.RoundToInt(_attackPenalty * 100f));
            case 3:
                return string.Format(baseDescription, Mathf.RoundToInt(-_attackPenalty * 100f), Mathf.RoundToInt(_oilPenalty * 100f));
            case 4:
                return string.Format(baseDescription, Mathf.RoundToInt(_oilPenalty * 100f));
        }

        return baseDescription;
    }

    public override void Initialize(TaskSettingsBase settings)
    {
        base.Initialize(settings);
    }

    public override void PlayerAnwer(int index)
    {
        if (_progress != 0)
        {
            TaskManager.instance.RemoveTask(this);
            return;
        }

        var taskSettings = (DieselDogsTaskSettings)_settings;

        switch (index)
        {
            case 0:
                _oilPenalty = Random.Range(taskSettings.minOilPenalty, taskSettings.maxOilPenalty);
                _progress = 1;
                break;
            case 1:
                _attackPenalty = Random.Range(taskSettings.minAttackPenalty, taskSettings.maxAttackPenalty);
                _progress = 2;
                break;
            case 2:
                float random = Random.Range(0f, 1f);
                if (random < 0.5f)
                {
                    _attackPenalty = -Random.Range(taskSettings.minAtackBoost3, taskSettings.maxAtackBoost3);
                    _oilPenalty = Random.Range(taskSettings.minOilPenalty31, taskSettings.maxOilPenalty31);
                    _progress = 3;
                }
                else
                {
                    _oilPenalty = Random.Range(taskSettings.minOilPenalty32, taskSettings.maxOilPenalty32);
                    _progress = 4;
                }
                break;
        }

        var oilModificator = (OilRigIncomeModificator)ModificatorManager.Instance.GetResourceModificator(new OilRigIncomeModificator());
        oilModificator.PercentageProductionDieselDogs -= _oilPenalty;
        
        var attackModificator = (GuardTowerAttackModificator)ModificatorManager.Instance.GetResourceModificator(new GuardTowerAttackModificator());
        attackModificator.AttackPowerInPercentDieselDogs -= _attackPenalty;

        TaskManager.instance.TaskUpdated(this);
    }

    public override bool UpdateTask()
    {
        return true;
    }
}
