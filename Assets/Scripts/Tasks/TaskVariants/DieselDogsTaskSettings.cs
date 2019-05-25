using UnityEngine;
using Tasks;

[CreateAssetMenu(fileName = "DieselDogs", menuName = "Tasks/DieselDogs")]
public class DieselDogsTaskSettings : BasicTaskSettings
{
    [Header("Answer1 parameters")]
    public float minOilPenalty;
    public float maxOilPenalty;

    [Header("Answer2 parameters")]
    public float minAttackPenalty;
    public float maxAttackPenalty;

    [Header("Answer3 parameters 1")]
    public int answer3OilPrice;
    public float minAtackBoost3;
    public float maxAtackBoost3;
    public float minOilPenalty31;
    public float maxOilPenalty31;

    [Header("Answer3 parameters 2")]
    public float minOilPenalty32;
    public float maxOilPenalty32;

    public override ITask CreateNewTask()
    {
        return new DieselDogsTask();
    }
}
