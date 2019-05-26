using UnityEngine;
using Tasks;

[CreateAssetMenu(fileName = "MysteriousTunnels", menuName = "Tasks/Events/MysteriousTunnels")]
public class MysteriousTunnelsEvent : EventSettingsBase
{
#pragma warning disable CS0649
    [SerializeField]
    protected int _needProductionBuildingsCount;
#pragma warning restore CS0649

    public override bool CheckConditions()
    {
        return true; // Todo: get player buildings count
    }
}
