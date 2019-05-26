using UnityEngine;
using Tasks;
using Assets.Scripts.Modificators.AttackModificators;

public class OldPipesTask : BasicTask
{
    float _oilPenalty;

    public override string GetTaskDescription()
    {
        string baseDescription = base.GetTaskDescription();
        switch (_progress)
        {
            case 1:
                return string.Format(baseDescription, Mathf.RoundToInt(_oilPenalty * 100f));
            case 3:
                return string.Format(baseDescription, Mathf.RoundToInt(_oilPenalty * 100f));
        }
        return baseDescription;
    }

    public override string GetAnswerText(int index)
    {
        if (_progress == 0)
        {
            switch (index)
            {
                case 1:
                    return string.Format(base.GetAnswerText(index), ((OldPipesTaskSettings)_settings).answer2OilPrice);
            }
        }

        return base.GetAnswerText(index);
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

        var taskSettings = (OldPipesTaskSettings)_settings;

        switch (index)
        {
            case 0:
                _oilPenalty = Random.Range(taskSettings.minOilPenalty1, taskSettings.maxOilPenalty1);
                _progress = 1;
                break;
            case 1:
                float random = Random.Range(0f, 1f);

                ResourceManager.Instance.Oil -= taskSettings.answer2OilPrice;

                if (random < 0.5f)
                {
                    _progress = 2;
                }
                else
                {
                    _oilPenalty = Random.Range(taskSettings.minOilPenalty2, taskSettings.maxOilPenalty2);
                    _progress = 3;
                }
                break;
        }

        var oilModificator = (OilRigIncomeModificator)ModificatorManager.Instance.GetResourceModificator(new OilRigIncomeModificator());
        oilModificator.PercentageProductionOldPipe -= _oilPenalty;

        TaskManager.instance.TaskUpdated(this);
    }

    public override bool IsAnswerOptionAllowed(int index)
    {
        if (_progress == 0)
            switch (index)
            {
                case 1:
                    return ResourceManager.Instance.Oil > ((OldPipesTaskSettings)_settings).answer2OilPrice;
            }
        return true;
    }

    public override bool UpdateTask()
    {
        return true;
    }
}
