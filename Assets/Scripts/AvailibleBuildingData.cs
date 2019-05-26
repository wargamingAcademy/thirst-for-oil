using System;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Карта мест на карте где можно строить здания
/// </summary>
[CreateAssetMenu(fileName = "AvailibleBuildingData", menuName = "AvailibleBuildingData", order = 55)]
public class AvailibleBuildingData : ScriptableObject
{
    public bool[,] IsAvailibleBuilding;
    private Tilemap availibleBuildingTilemap;
    private Tilemap availibleBuildingForShowTilemap;
    public Tile activeCell;
    public Tile inactiveCell;
    public Vector2Int availibleBuildingOffset;
    private UIOilController uiController;
    private LevelManager levelManager;

    public void Initialize(Vector2Int worldSize)
    {
        levelManager = FindObjectOfType<LevelManager>();
        uiController = FindObjectOfType<UIOilController>();
        GameObject tilemapGameObject = GameObject.Find(ObjectnamesConstant.AVAILIBLE_BUILDING);
        availibleBuildingTilemap = tilemapGameObject.GetComponent<Tilemap>();
        GameObject availibleBuildingForShowGameObject =
            GameObject.Find(ObjectnamesConstant.AVAILIBLE_BUILDING_FOR_SHOW);
        availibleBuildingForShowTilemap = availibleBuildingForShowGameObject.GetComponent<Tilemap>();
        if (availibleBuildingTilemap == null)
        {
            throw new ArgumentNullException();
        }

        BoundsInt bounds = availibleBuildingTilemap.cellBounds;
        availibleBuildingOffset=new Vector2Int(bounds.x,bounds.y);
        var forShowBound=availibleBuildingForShowTilemap.cellBounds;
       /* forShowBound.x = availibleBuildingOffset.x;
        forShowBound.y = availibleBuildingOffset.y;*/

        IsAvailibleBuilding = new bool[worldSize.x, worldSize.y];
        for (int x = 0; x < worldSize.x; x++)
        {
            for (int y = 0; y < worldSize.y; y++)
            {
                IsAvailibleBuilding[x, y] = true;
            }
        }

        for (int x = 0; x < levelManager.worldSize.x; x++)
        {
            for (int y = 0; y < levelManager.worldSize.y; y++)
            {
               /* Vector2Int size = new Vector2Int();
                size.x = availibleBuildingTilemap.size.x % 2 > 0
                    ? availibleBuildingTilemap.size.x / 2 + 1
                    : availibleBuildingTilemap.size.x / 2;
                size.y = availibleBuildingTilemap.size.y % 2 > 0
                    ? availibleBuildingTilemap.size.y / 2 + 1
                    : availibleBuildingTilemap.size.y / 2 + 1;*/

                TileBase tileBase =
                    availibleBuildingTilemap.GetTile(new Vector3Int(x +levelManager.offset.x, y  + levelManager.offset.y, 0));
                if (tileBase == null)
                {
                    IsAvailibleBuilding[x, y] = false;
                    continue;
                }

                switch (tileBase.name)
                {
                    case "noBuilding":
                        IsAvailibleBuilding[x, y] = false;
                        break;
                    case "building":
                        IsAvailibleBuilding[x, y] = true;
                        break;
                }
            }
        }
    }

    public void SetAvailibleBuildingForShow(bool[,] availibleCells,Vector2Int offset)
    {
        for (int i = 0; i < availibleCells.GetLength(0); i++)
        {
            for (int j = 0; j < availibleCells.GetLength(1); j++)
            {
                if (availibleCells[i, j])
                {
                    availibleBuildingForShowTilemap.SetTile(new Vector3Int(i+offset.x,j+offset.y,0), activeCell);
                }
                else
                {
                    availibleBuildingForShowTilemap.SetTile(new Vector3Int(i + offset.x, j + offset.y,0), inactiveCell);
                }
            }
        }
    }

    public void ShowAvailibleCells(bool isVisible,GeneralBuilding building)
    {
        if (isVisible)
        {
            availibleBuildingForShowTilemap.enabled = true;
            uiController.HidePriceBuildingOnBar();
            uiController.ShowPriceBuildingOnBar(building.GetPrice() * Constants.ANCHOR_MAX_Y / 100);
        
        }
        else
        {
            availibleBuildingForShowTilemap.ClearAllTiles();
            uiController.HidePriceBuildingOnBar();
        }
    }

    public Vector2Int GetTilemapOffset()
    {
        Camera camera = FindObjectOfType<Camera>();
        /*camera.rect.min;
        availibleBuildingTilemap.transform.position;
        availibleBuildingTilemap.cellBounds;
        availibleBuildingTilemap.cellSize;*/
        Vector3 cameraBoundMin = camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 distance=new Vector2(-availibleBuildingTilemap.cellBounds.xMin* availibleBuildingTilemap.cellSize.x + camera.rect.position.x,
            -availibleBuildingTilemap.cellBounds.yMin * availibleBuildingTilemap.cellSize.y + camera.rect.position.y);
        Vector2Int offset = new Vector2Int((int)Math.Ceiling(distance.x / availibleBuildingTilemap.cellSize.x),
            (int)Math.Ceiling(distance.y / availibleBuildingTilemap.cellSize.y));
        return new Vector2Int(offset.x-availibleBuildingTilemap.cellBounds.x, offset.y - availibleBuildingTilemap.cellBounds.y);
    }
}