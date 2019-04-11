using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
[CreateAssetMenu(fileName = "GridTilemap", menuName = "GridTilemap", order = 56)]
public class GridTilemap : ScriptableObject
{
    public Tile grid;
    public GameObject tilemapGameobject;
    private Tilemap tilemap;
    public void Initialize(Vector2Int worldSize,Vector2Int offset)
    {   
        tilemap = tilemapGameobject.GetComponent<Tilemap>();
        tilemap.ClearAllTiles();       
        for (int x = 0; x < worldSize.x; x++)
        {
            for (int y = 0; y < worldSize.y; y++)
            {
                tilemap.SetTile(new Vector3Int(x+offset.x,y+offset.y, 0), grid);
            }
        }
    }
}