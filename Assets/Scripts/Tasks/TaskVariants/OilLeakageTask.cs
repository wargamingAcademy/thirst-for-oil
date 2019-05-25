using UnityEngine;
using Tasks;

public class OilLeakageTask : BasicTask
{
    int _oilLeakage;

    public override string GetTaskDescription()
    {
        return string.Format(base.GetTaskDescription(), _oilLeakage);
    }

    public override void Initialize(TaskSettingsBase settings)
    {
        base.Initialize(settings);

        var leakageSettings = (OilLeakageTaskSettings)_settings;
        _oilLeakage = Random.Range(leakageSettings.minOilLeakage, leakageSettings.maxOilLeakage);
    }

    public override void PlayerAnwer(int index)
    {
        base.PlayerAnwer(index);            
    }

    public override bool UpdateTask()
    {
        if (_progress == 0)
        {
            ResourceManager.Instance.Oil -= _oilLeakage;
            return true;
        }
        return false;
    }
}
