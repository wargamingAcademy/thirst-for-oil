using UnityEngine;
using Tasks;

[CreateAssetMenu(fileName = "WeGonaDie", menuName = "Tasks/WeGonaDie")]
public class WeGonaDieTaskSettings : BasicTaskSettings
{
    [Header("Answer1 parameters")]
    public float minBuildingPricePenalty;
    public float maxBuildingPricePenalty;
    public float minOilProductPenalty;
    public float maxOilProductPenalty;

    [Space(5)]
    public float nextTaskChance;
    public BasicTaskSettings nextTask;

    public override ITask CreateNewTask()
    {
        return new WeGonaDieTask();
    }
}
