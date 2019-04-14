using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingUILoader : MonoBehaviour
{
    public Building building;
    private GeneralBuilding generalBuilding;
    public void Start()
    {
        Text text = gameObject.GetComponentInChildren<Text>();
        Button button = gameObject.GetComponentInChildren<Button>();
        
        generalBuilding= GetBuilding(building);
        text.text = generalBuilding.GetDescription();
        button.image.sprite = generalBuilding.GetSprite(TileNames.OIL_RIG);
       
        button.onClick.AddListener(TaskOnClick); 
    }

    public void TaskOnClick()
    {
        CursorDrawer cursorDrawer = FindObjectOfType<CursorDrawer>();

        generalBuilding.ConstructBuilding(cursorDrawer.currentSelectedTile);
    }
    public GeneralBuilding GetBuilding(Building building)
    {
        switch (building)
        {
            case Building.oilRig:return new OilRig();
            case Building.guardTower: return new GuardTower();
        }
        throw new ArgumentException("нет такого здания");
    }
    
}