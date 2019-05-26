using UnityEngine;
using Tasks;
using Assets.Scripts.Modificators.BuildingsModificators.PriceBuildingModificators;

public class MysteriousTunnelsTask : BasicTask
{
    float _buildingPriceBoost;

    public override string GetTaskDescription()
    {
        string baseDescription = base.GetTaskDescription();
        switch (_progress)
        {
            case 1:
                return string.Format(baseDescription, Mathf.RoundToInt(_buildingPriceBoost * 100f));
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

        var taskSettings = (MysteriousTunnelsTaskSettings)_settings;

        switch (index)
        {
            case 0:
                _buildingPriceBoost = Random.Range(taskSettings.minBuildingPriceBoost, taskSettings.maxBuildingPriceBoost);
                _progress = 1;

                float random = Random.Range(0f, 1f);

                if (random < taskSettings.nextTaskChance)
                    EventGenerator.instance.AddDelayedEvent(0, taskSettings.nextTask);

                break;
            case 1:
                TaskManager.instance.RemoveTask(this);
                return;
        }

        var modificator = (PriceBuildingModificator)ModificatorManager.Instance.GetResourceModificator(new PriceBuildingModificator());
        modificator.MysteriousTunnelsPriceCoeff -= _buildingPriceBoost;

        TaskManager.instance.TaskUpdated(this);
    }

    public override bool UpdateTask()
    {
        return true;
    }
}
