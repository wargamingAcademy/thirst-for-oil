using UnityEngine;
using Tasks;

[CreateAssetMenu(fileName = "MysteriousTunnels", menuName = "Tasks/MysteriousTunnels")]
public class MysteriousTunnelsTaskSettings : BasicTaskSettings
{
    [Header("Answer1 parameters")]
    public float minBuildingPriceBoost;
    public float maxBuildingPriceBoost;

    [Header("Answer2 parameters")]
    public float nextTaskChance;
    public BasicTaskSettings nextTask;

    public override ITask CreateNewTask()
    {
        return new MysteriousTunnelsTask();
    }
}
