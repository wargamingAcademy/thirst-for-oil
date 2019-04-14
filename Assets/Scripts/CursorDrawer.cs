using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System;
using Gamekit2D;

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
    void OnGUI()
    {
        Bounds bounds = hoverCellTilemap.localBounds;
        Vector2 mousePos = Input.mousePosition;
        GUI.Label(new Rect(30f, 440.0f, 200.0f, 25.0f), cursorPosition.x + "  " + cursorPosition.y);
        GUI.Label(new Rect(30f, 470.0f, 200.0f, 25.0f), worldPosition.x + "  " + worldPosition.y);
        GUI.Label(new Rect(30f, 500.0f, 200.0f, 25.0f), bounds.min.x + "  " + bounds.min.y);
        GUI.Label(new Rect(30f, 530.0f, 200.0f, 25.0f), mousePos.x + "  " + mousePos.y);
        GUI.Label(new Rect(30f, 560.0f, 200.0f, 25.0f), hoverCellTilemap.cellSize.x + "  " + hoverCellTilemap.cellSize.y);
    }
}