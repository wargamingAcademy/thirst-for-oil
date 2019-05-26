using UnityEngine;
using Tasks;

[CreateAssetMenu(fileName = "OldPipes", menuName = "Tasks/OldPipes")]
public class OldPipesTaskSettings : BasicTaskSettings
{
    [Header("Answer1 parameters")]
    public float minOilPenalty1;
    public float maxOilPenalty1;

    [Header("Answer2 parameters 2")]
    public int answer2OilPrice;
    public float minOilPenalty2;
    public float maxOilPenalty2;

    public override ITask CreateNewTask()
    {
        return new OldPipesTask();
    }
}
