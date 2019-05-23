using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System;
using Gamekit2D;
using UnityEngine.Experimental.UIElements;

/// <summary>
/// Скрипт отвечающий за отрисовку квадратного курсора на карте
/// </summary>
public class CursorDrawer : MonoBehaviour
{
    public Tilemap hoverCellTilemap;
    public Tilemap selectedCellTilemap;
    public Tile hoverCell;
    public Tile selectedCell;
    public Tile selectedForBuildingCellActive;
    public Tile selectedForBuildingCellInactive;
    public Vector2Int cursorPosition;
    public Vector2Int currentSelectedTile;
    private Camera camera;
    private Vector3 worldPosition;
    private LevelManager levelManager;


    
    private bool isBuildingStarted;
    private GeneralBuilding building;

    void Start()
    {
        BuildingUI.StartBuildingEvent += OnStartBuildingEvent;
        BuildingUI.ChancelBuildingEvent += OnChancelBuildingEvent;
        levelManager = FindObjectOfType<LevelManager>();
        hoverCellTilemap.size=new Vector3Int(-15,15,0);
    }

    void OnStartBuildingEvent(GeneralBuilding generalBuilding)
    {
        isBuildingStarted = true;
        building = generalBuilding;
    }

    void OnChancelBuildingEvent()
    {
        isBuildingStarted = false;
        levelManager.availibleBuildingData.ShowAvailibleCells(false);
    }

    void FixedUpdate()
    {
        if ((PlayerInput.Instance.ChancelAction.Down) && (isBuildingStarted))
        {
            OnChancelBuildingEvent();
        }
    }

    void OnGUI()
    {
        Vector2 mousePos = Input.mousePosition;
        GUI.Label(new Rect(100, 100, 200, 40),"x:"+ camera.ScreenToWorldPoint(mousePos).x+ " y:" + camera.ScreenToWorldPoint(mousePos).y);
        Bounds bounds = hoverCellTilemap.localBounds;
        worldPosition = camera.ScreenToWorldPoint(mousePos);
        cursorPosition = new Vector2Int(
            (int)Math.Ceiling((worldPosition.x /*+ Constants.TILEMAP_OFFSET.x*/) /
                              hoverCellTilemap.cellSize.x),
            (int)Math.Ceiling((worldPosition.y /*+ Constants.TILEMAP_OFFSET.y*/) /
                              hoverCellTilemap.cellSize.y));
        GUI.Label(new Rect(100, 150, 200, 40), "x:" + cursorPosition.x+ " y:" + cursorPosition.y);
        GUI.Label(new Rect(100, 200, 200, 40), "x:" + (bounds.min.x - worldPosition.x) + " y:" + (bounds.min.y - worldPosition.y));
        GUI.Label(new Rect(100, 230, 200, 40), "bounds.min.x:" + bounds.min.x + " bounds.min.y:" + bounds.min.y);
        GUI.Label(new Rect(100, 260, 200, 40), "worldPosition.x:" + worldPosition.x + " worldPosition.y:" + worldPosition.y);
        GUI.Label(new Rect(100, 290, 200, 40), "worldPosition.x:" + ((bounds.min.x - worldPosition.x)/hoverCellTilemap.cellSize.x) +
                                               " worldPosition.y:" + ((bounds.min.y - worldPosition.y) / hoverCellTilemap.cellSize.y));
    }
    void Update()
    {
       
        hoverCellTilemap.ClearAllTiles();
      //  hoverCellTilemap.size=new Vector3Int(10,10,0);
        Bounds bounds = hoverCellTilemap.localBounds;
        Vector2 mousePos = Input.mousePosition;
        if (camera == null)
        {
            camera = GameObject.FindObjectOfType<Camera>();
        }

        worldPosition = camera.ScreenToWorldPoint(mousePos);
        cursorPosition = new Vector2Int(
            (int)Math.Ceiling((worldPosition.x /*+ Constants.TILEMAP_OFFSET.x*/) /
                                hoverCellTilemap.cellSize.x),
            (int)Math.Ceiling((worldPosition.y /*+ Constants.TILEMAP_OFFSET.y*/) /
                                hoverCellTilemap.cellSize.y));
        if (!isBuildingStarted)
        {
            hoverCellTilemap.SetTile(new Vector3Int(cursorPosition.x-1, cursorPosition.y-1, 0), hoverCell);
            if (PlayerInput.Instance.CellSelected.Down)
            {
                currentSelectedTile = cursorPosition;
                selectedCellTilemap.ClearAllTiles();
                selectedCellTilemap.SetTile(new Vector3Int(cursorPosition.x - 1, cursorPosition.y - 1, 0), selectedCell);
            }
        }
        else
        {
            if (building.CheckPosssibilityBuilding(cursorPosition))
            {
                hoverCellTilemap.SetTile(new Vector3Int(cursorPosition.x - 1, cursorPosition.y - 1, 0),
                    selectedForBuildingCellActive);
                if (PlayerInput.Instance.CellSelected.Down)
                {
                    building.ConstructBuilding(currentSelectedTile);
                }
            }
            else
            {
                hoverCellTilemap.SetTile(new Vector3Int(cursorPosition.x - 1, cursorPosition.y - 1, 0),
                selectedForBuildingCellInactive);
            }
        }
    }
}