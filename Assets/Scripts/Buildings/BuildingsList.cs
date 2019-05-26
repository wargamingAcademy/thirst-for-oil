using System.Collections.Generic;

public static class BuildingsList
{
    private const int CURRENT_COUNT_BUILDING = 2;
    private static Dictionary<Building,GeneralBuilding> buildingDictionary;
    private static bool isInitialized;
    public static void Initialize()
    {
        buildingDictionary=new Dictionary<Building, GeneralBuilding>();
        buildingDictionary[Building.guardTower]=new GuardTower();
        buildingDictionary[Building.oilRig]=new OilRig();
    }

    public static Dictionary<Building, GeneralBuilding> GetBuildingDictionary()
    {
        if (!isInitialized)
        {
            isInitialized = true;
            Initialize();
        }
        return buildingDictionary;
    }
}