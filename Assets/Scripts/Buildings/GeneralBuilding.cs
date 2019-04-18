using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Хранит общую информацию для зданий
/// </summary>
public abstract class GeneralBuilding
{
    protected UIController uiController;
    protected LevelManager levelManager;
    protected ResourceManager resourceManager;
   


    public GeneralBuilding()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        resourceManager = GameObject.FindObjectOfType<ResourceManager>();
        uiController = GameObject.FindObjectOfType<UIController>();

    }
    

    /// <summary>
    /// Ищем тайл по имени
    /// </summary>
    /// <param name="name">имя спрайта</param>
    /// <returns></returns>
    public Tile  GetTile(string name)
    {
        foreach (Tile tile in levelManager.tiles)
        {
            if (tile.name == name)
            {
                return tile;
            }
        }
        throw new FileNotFoundException("Спрайт не найден");
    }

    public Sprite GetSprite(string name)
    {
        foreach (Sprite sprite in levelManager.sprites)
        {
            if (sprite.name == name)
            {
                return sprite;
            }
        }
        return null;
    }

    /// <summary>
    /// Построить здание
    /// </summary>
    /// <param name="coordinate">позиция строения</param>
    /// <param name="building">здание</param>
    /// <returns>true если успешно построили</returns>
    public bool ConstructBuilding(Vector2Int coordinate)
    {
        /*TileBase tile = levelManager.fogeOfWarTilemap.GetTile(new Vector3Int(coordinate.x, coordinate.y, 0));
        if (tile == null)
        {
            return false;
        }

        bool isFogeOfWar = tile.name == TileNames.FOGE;
        bool isNotAvailibleBuildBuilding = !levelManager.availibleBuildingTilemap.IsAvailibleBuilding[coordinate.x, coordinate.y];
        if ((isNotAvailibleBuildBuilding) && (isFogeOfWar))
        {
            return false;
        }*/
        Vector2Int position = new Vector2Int(coordinate.x - levelManager.resourceTilemap.tilemap.cellBounds.min.x, coordinate.y - levelManager.resourceTilemap.tilemap.cellBounds.min.y);
        if (!IsCanBeBuild(position))
        {
            return false;
        }

        if (resourceManager.Oil < GetPrice())
        {
            return false;
        }
        levelManager.availibleBuildingTilemap.IsAvailibleBuilding[position.x, position.y] = false;
        levelManager.buildingTilemap.SetTile(new Vector3Int(coordinate.x, coordinate.y, 0), this.GetTile());
        levelManager.resourceTilemap.tilemap.SetTile(new Vector3Int(coordinate.x, coordinate.y, 0),null);
        float priceBuilding= GetPrice();
        resourceManager.Oil -= priceBuilding;
        OnBuilding();
       // TurnController.TurnEndEvent += OnEndTurn;
        return true;
    }

    public abstract TileBase GetTile();
    public abstract Sprite GetSprite();
    public abstract string GetDescription();
    public abstract float GetPrice();
    public abstract bool IsCanBeBuild(Vector2Int position);
    public abstract void OnEndTurn();
    public abstract void OnBuilding();

}