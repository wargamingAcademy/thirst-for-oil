using UnityEngine;
using Tasks;

[CreateAssetMenu(fileName = "OilLeakage", menuName = "Tasks/OilLeakage")]
public class OilLeakageTaskSettings : BasicTaskSettings
{
    public int minOilLeakage;
    public int maxOilLeakage;

    public override ITask CreateNewTask()
    {
        return new OilLeakageTask();
    }
}
