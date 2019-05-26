using UnityEngine;
using Tasks;

[CreateAssetMenu(fileName = "WeHaveALOss", menuName = "Tasks/WeHaveALOss")]
public class WeHaveALOssTaskSettings : BasicTaskSettings
{
    [Header("Answer1 parameters")]
    public float minBuildingPriceBoost;
    public float maxBuildingPriceBoost;
    public BasicTaskSettings nextTask1;

    [Header("Answer2 parameters")]
    public BasicTaskSettings nextTask2;

    public override ITask CreateNewTask()
    {
        return new WeHaveALOssTask();
    }
}
