using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class BuildingUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Building building;
    public UIOilController uiController;
    private GeneralBuilding generalBuilding;
    public void Start()
    {
        Text text = gameObject.GetComponentInChildren<Text>();
        Button button = gameObject.GetComponentInChildren<Button>();
        
        generalBuilding= GetBuilding(building);
        text.text = generalBuilding.GetDescription();
       
        button.onClick.AddListener(TaskOnClick); 
    }

    /// <summary>
    /// Событие нажатия на кнопку строительства 
    /// </summary>
    public void TaskOnClick()
    {
        var cursorDrawer = FindObjectOfType<CursorDrawer>();
        var newBuilding = GetBuilding(building);
        newBuilding.ConstructBuilding(cursorDrawer.currentSelectedTile);
    }

    /// <summary>
    /// TODO:переделать
    /// </summary>
    /// <param name="building"></param>
    /// <returns></returns>
    public GeneralBuilding GetBuilding(Building building)
    {
        switch (building)
        {
            case Building.oilRig:return new OilRig();
            case Building.guardTower: return new GuardTower();
        }
        throw new ArgumentException("нет такого здания");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiController.ShowPriceBuildingOnBar(generalBuilding.GetPrice()*Constants.ANCHOR_MAX_Y/100);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uiController.HidePriceBuildingOnBar();
    }
}