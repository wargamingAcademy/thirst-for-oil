using System;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class BuildInterfaceBuilder:MonoBehaviour
{
    private const int COUNT_BUILD_BUTTONS_IN_HEIGHT = 11;
    private const float WIDTH_PANEL_IN_NORMALIZED_PERCENT = 0.4f;
    private const float INDENT_PANEL_X = 0.3f;
    private const float INDENT_PANEL_Y = 0f;
    private const float INDENT_PANEL_X_IN_PIXELS = 20;
    private const float INDENT_PANEL_Y_IN_PIXELS = 20;
    private const float INDENT_BUTTON_Y_IN_PIXELS = 15;
    private const float GAP_BUTTON_IN_PIXELS = 15;
    [SerializeField]
    private GameObject buildingPrefab;
    [SerializeField]
    private GameObject background;
    private BuildingPool buildingPool;
    private int page;
    [SerializeField]
    private Sprite emptyIcon;

    public void ShowBuildInterface()
    {        
        int height=Screen.height/COUNT_BUILD_BUTTONS_IN_HEIGHT;
        float widthPanelInPixels = Screen.width * WIDTH_PANEL_IN_NORMALIZED_PERCENT - INDENT_PANEL_X;
        float indentButtonYNormalized = INDENT_BUTTON_Y_IN_PIXELS / Screen.height;
        float gapButtonYNormalized = GAP_BUTTON_IN_PIXELS / Screen.width;
        int countBuildingInPanel=(int)Mathf.Floor((widthPanelInPixels+GAP_BUTTON_IN_PIXELS-2*INDENT_PANEL_Y_IN_PIXELS)/(height+GAP_BUTTON_IN_PIXELS));
        widthPanelInPixels = countBuildingInPanel * (GAP_BUTTON_IN_PIXELS + height) + 2 * INDENT_PANEL_Y_IN_PIXELS- GAP_BUTTON_IN_PIXELS;
        RectTransform rt = buildingPrefab.GetComponent<RectTransform>();
        rt.sizeDelta= new Vector2(height,height);
        var buttonGameobject = buildingPrefab.transform.GetChild(0);
        var buttonRect=buttonGameobject.GetComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(height, height);
        
        float normalizedWidth = (float)height / Screen.width;
        float normalizedHeight = (float)height / Screen.height;
        buildingPool=BuildingPool.GetObjectPool(buildingPrefab, countBuildingInPanel);
        var buildings = BuildingsList.GetBuildingDictionary();

        int countBuilding = buildings.Count;
        int countFilled;
        Building[] buildingList = new Building[buildings.Keys.Count];
        buildings.Keys.CopyTo(buildingList, 0);
        if (page != 0)
        {
            if (buildings.Count / page >= countBuildingInPanel)
            {
                countFilled = countBuildingInPanel;
            }
            else
            {
                countFilled = buildings.Count - page * countBuildingInPanel;
            }
        }
        else
        {
            countFilled = buildings.Count - page * countBuildingInPanel;
        }
        float normalizedIndentY = INDENT_PANEL_Y_IN_PIXELS / (float)Screen.height;
        for (int i = 0; i < countBuildingInPanel; i++)
        {
            BuildingObject building = buildingPool.Pop();
            Button button = building.instance.GetComponentInChildren<Button>();
            BuildingUI buildingUI = building.instance.GetComponentInChildren<BuildingUI>();
           // buildingUI.uiController = FindObjectOfType<UIOilController>();
            var buttonGO = building.instance.transform.GetChild(0);
            var buttonScript = buttonGO.GetComponent<Button>();
            var colors = buttonScript.colors;
            var tooltip = building.instance.GetComponentInChildren<Tooltip>();
            if (countFilled > 0)
            {
                countFilled--;

                buildingUI.building = buildingList[page * countBuildingInPanel + i];
                GeneralBuilding generalBuilding;                
                buildings.TryGetValue(buildingUI.building,out generalBuilding);
                buildingUI.generalBuilding = generalBuilding;
                button.image.sprite = generalBuilding.GetSpriteIcon();
                colors.normalColor = new Color(255, 255, 255, 255);
                buttonScript.colors = colors;
                button.onClick.AddListener(buildingUI.TaskOnClick);
                tooltip.Enable();
                tooltip.buiding = generalBuilding;
            }
            else
            {
                tooltip.Disable();
                buildingUI.building = Building.none;
                button.image.sprite = emptyIcon;              
                colors.normalColor = new Color(255, 255, 255,255);
                buttonScript.colors = colors;
            }
            var rectTransform=building.instance.GetComponent<RectTransform>();
            float normalizedGap = GAP_BUTTON_IN_PIXELS / (float) Screen.width;
            float normalizedIndentX = INDENT_PANEL_X_IN_PIXELS / (float) Screen.width;
          
            rectTransform.anchorMax=new Vector2(INDENT_PANEL_X+normalizedIndentX+(normalizedWidth+normalizedGap)*i+normalizedWidth/2,
                INDENT_PANEL_Y+normalizedIndentY+normalizedHeight/2);
            rectTransform.anchorMin = rectTransform.anchorMax;
            rectTransform.sizeDelta= new Vector2(height,height);
           //rectTransform.position=new Vector3(0,0,0);
            rectTransform.anchoredPosition=new Vector2(0,0);
        }
        var backgroundRect = background.GetComponent<RectTransform>();
        //backgroundRect.sizeDelta = new Vector2(widthPanelInPixels, height+50);
        float widthPanelInNormalizedPercent = widthPanelInPixels / (float) Screen.width;

        backgroundRect.anchorMax = new Vector2(INDENT_PANEL_X + widthPanelInNormalizedPercent, INDENT_PANEL_Y + normalizedHeight+2* normalizedIndentY);
        backgroundRect.anchorMin = new Vector2(INDENT_PANEL_X, INDENT_PANEL_Y);

        /*  private const int COUNT_COPY_OBJECT_IN_POOLS = 30;
          [SerializeField]
          private GameObject[] obstacles;
          [SerializeField]
          float g;
          [SerializeField]
          Sprite backGround;
          private ObstaclePool[] obstaclePools;
      
          public IEnumerator GenerateObstacles()
          {
              if ((obstaclePools == null) || (obstaclePools.Length) < obstacles.Length)
              {
                  obstaclePools = new ObstaclePool[obstacles.Length];
              }
              for (int i = 0; i < obstacles.Length; i++)
              {
                  if (obstaclePools[i] == null)
                  {
                      obstaclePools[i] = ObstaclePool.GetObjectPool(obstacles[i], COUNT_COPY_OBJECT_IN_POOLS);
                  }
              }
      
              while (Game.Instance.State == State.Game)
              {
                  int nextObstacle = UnityEngine.Random.Range(0, obstacles.Length);
                  Obstacle obstacle = obstacles[nextObstacle].GetComponent<Obstacle>();
                  obstaclePools[nextObstacle].Pop(obstacle.StartPosition);
                  yield return new WaitForSeconds(4f);
              }
          }*/
    }
}