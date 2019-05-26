using UnityEngine;
using Tasks;
using Assets.Scripts.Buildings;
using Assets.Scripts.Modificators.BuildingsModificators.PriceBuildingModificators;

public class WeGonaDieTask : BasicTask
{
    float _buildingPricePenalty;
    float _oilProductionPenalty;

    public override string GetTaskDescription()
    {
        string baseDescription = base.GetTaskDescription();
        switch (_progress)
        {
            case 0:
                return string.Format(baseDescription, Mathf.RoundToInt(_buildingPricePenalty * 100f), Mathf.RoundToInt(_oilProductionPenalty * 100f));
        }
        return baseDescription;
    }

    public override void Initialize(TaskSettingsBase settings)
    {
        base.Initialize(settings);

        var taskSettings = (WeGonaDieTaskSettings)_settings;

        _buildingPricePenalty = Random.Range(taskSettings.minBuildingPricePenalty, taskSettings.maxBuildingPricePenalty);
        _oilProductionPenalty = Random.Range(taskSettings.minOilProductPenalty, taskSettings.maxOilProductPenalty);

        var buildingModificator = (PriceBuildingModificator)ModificatorManager.Instance.GetResourceModificator(new PriceBuildingModificator());
        buildingModificator.WeAllGonnaDiePriceCoeff += _buildingPricePenalty;

        var OilModificator = (OilRigIncomeModificator)ModificatorManager.Instance.GetResourceModificator(new OilRigIncomeModificator());
        OilModificator.PercentageProductionWeAllGonnaDie -= _oilProductionPenalty;
    }

    public override void PlayerAnwer(int index)
    {
        if (_progress != 0)
        {
            TaskManager.instance.TaskUpdated(this);
            return;
        }

        _progress = 1;

        TaskManager.instance.TaskSuspended(this);
    }

    public override bool UpdateTask()
    {
        float random = Random.Range(0f, 1f);

        if (random < ((WeGonaDieTaskSettings)_settings).nextTaskChance)
        {
            EventGenerator.instance.AddDelayedEvent(0, ((WeGonaDieTaskSettings)_settings).nextTask);
            TaskManager.instance.RemoveTask(this);
        }
        else
        {
            Debug.Log("Destroy random building");
            //BuildingManager.DestroyRandomBuilding();
        }

        return false;
    }
}
