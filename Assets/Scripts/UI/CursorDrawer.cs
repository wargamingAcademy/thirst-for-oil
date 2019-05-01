using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System;
using Gamekit2D;

/// <summary>
/// Скрипт отвечающий за отрисовку квадратного курсора на карте
/// </summary>
public class CursorDrawer : MonoBehaviour
{
    public Tilemap hoverCellTilemap;
    public Tilemap selectedCellTilemap;
    public Tile hoverCell;
    public Tile selectedCell;
    public Vector2Int cursorPosition;
    public Vector2Int currentSelectedTile;
    private Camera camera;
    private Vector3 worldPosition;

    void Update()
    {
        hoverCellTilemap.ClearAllTiles();
        Bounds bounds = hoverCellTilemap.localBounds;
        Vector2 mousePos = Input.mousePosition;
        if (camera == null)
        {
            camera = GameObject.FindObjectOfType<Camera>();
        }
        worldPosition = camera.ScreenToWorldPoint(mousePos);
        cursorPosition = new Vector2Int(-(int)Math.Ceiling((bounds.min.x - worldPosition.x /*+ Constants.TILEMAP_OFFSET.x*/) / hoverCellTilemap.cellSize.x),
            -(int)Math.Ceiling((bounds.min.y - worldPosition.y /*+ Constants.TILEMAP_OFFSET.y*/) / hoverCellTilemap.cellSize.y));
       
        hoverCellTilemap.SetTile(new Vector3Int(cursorPosition.x,cursorPosition.y,0),hoverCell);
        if (PlayerInput.Instance.CellSelected.Down)
        {
            currentSelectedTile = cursorPosition;
            selectedCellTilemap.ClearAllTiles();
            selectedCellTilemap.SetTile(new Vector3Int(cursorPosition.x, cursorPosition.y, 0), selectedCell);
        }
    }
}