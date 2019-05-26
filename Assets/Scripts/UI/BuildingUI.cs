using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class BuildingUI : MonoBehaviour//,IPointerEnterHandler,IPointerExitHandler
{

    public Building building;

    public GeneralBuilding generalBuilding;
    private GeneralBuilding[] buildings;
    public delegate void BuildingDelegate(GeneralBuilding building);

    public delegate void ChancelBuildingDelegate();
    public static event BuildingDelegate StartBuildingEvent;
    public static event ChancelBuildingDelegate ChancelBuildingEvent;

    private LevelManager levelManager;

    public void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    /* public void Start()
     {
         Text text = gameObject.GetComponentInChildren<Text>();
         Button button = gameObject.GetComponentInChildren<Button>();

         generalBuilding= GetBuilding(building);
         text.text = generalBuilding.GetDescription();

         button.onClick.AddListener(TaskOnClick); 

     }*/

    /// <summary>
    /// Событие нажатия на кнопку строительства 
    /// </summary>
    public void TaskOnClick()
    {
       bool[,] availibleCells= generalBuilding.GetAvailibleCells();
      /* Vector3Int offset=new Vector3Int(generalBuilding.levelManager.availibleBuildingData.availibleBuildingOffset.x,
           generalBuilding.levelManager.availibleBuildingData.availibleBuildingOffset.y,0);*/
       generalBuilding.levelManager.availibleBuildingData.SetAvailibleBuildingForShow(availibleCells,levelManager.offset );
       generalBuilding.levelManager.availibleBuildingData.ShowAvailibleCells(true,generalBuilding);
       StartBuildingEvent(generalBuilding);
      
        /* CursorDrawer cursorDrawer = FindObjectOfType<CursorDrawer>();

         generalBuilding.ConstructBuilding(cursorDrawer.currentSelectedTile);*/
        /*var cursorDrawer = FindObjectOfType<CursorDrawer>();
        var newBuilding = GetBuilding(building);
        newBuilding.ConstructBuilding(cursorDrawer.currentSelectedTile);*/

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

  /*  public void OnPointerEnter(PointerEventData eventData)
    {
        uiController.ShowPriceBuildingOnBar(generalBuilding.GetPrice()*Constants.ANCHOR_MAX_Y/100);
    }*/

   /* public void OnPointerExit(PointerEventData eventData)
    {
        uiController.HidePriceBuildingOnBar();
    }*/
}