using System;
﻿using System.IO;
using Assets.Scripts.Buildings;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Базовый класс хранящий информацию о зданиях
/// </summary>
[Serializable]
public abstract class GeneralBuilding
{
    protected UIOilController uiController;
    public LevelManager levelManager;
    protected ResourceManager resourceManager;
    protected BuildingManager buildingManager;
    public Vector2Int Position { get; private set; }

   
    public GeneralBuilding()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        resourceManager = GameObject.FindObjectOfType<ResourceManager>();
        uiController = GameObject.FindObjectOfType<UIOilController>();
        buildingManager = GameObject.FindObjectOfType<BuildingManager>();
    }
      
    /// <summary>
    /// Построить здание
    /// </summary>
    /// <param name="coordinate">позиция строения</param>
    /// <param name="building">здание</param>
    /// <returns>true если успешно построили</returns>
    public bool ConstructBuilding(Vector2Int coordinate)
    {
        Vector2Int position = new Vector2Int(coordinate.x - levelManager.resourceData.tilemap.cellBounds.min.x, coordinate.y - levelManager.resourceData.tilemap.cellBounds.min.y);
        if (!IsCanBeBuild(coordinate))
        {
            return false;
        }

        if (resourceManager.Oil < GetPrice())
        {
            return false;
        }
        Position = coordinate;
        levelManager.availibleBuildingData.IsAvailibleBuilding[coordinate.x-levelManager.offset.x, coordinate.y - levelManager.offset.y] = false;
        levelManager.buildingData.SetTile(new Vector3Int(coordinate.x, coordinate.y, 0), this.GetTile());
        levelManager.resourceData.tilemap.SetTile(new Vector3Int(coordinate.x, coordinate.y, 0),null);
       
        float priceBuilding= GetPrice();
        resourceManager.Oil -= priceBuilding;
        buildingManager.AddBuilding(this);
        OnBuilding();
       // TurnController.TurnEndEvent += OnEndTurn;
        return true;
    }

    public abstract TileBase GetTile();
    public abstract Sprite GetSprite();
    public abstract Sprite GetSpriteIcon();
    /// <summary>
    /// Получить описание здания 
    /// </summary>
    public abstract string GetDescription();

    /// <summary>
    /// Стоимость здания в нефти
    /// </summary>
    public abstract float GetPrice();

    /// <summary>
    /// Возвращает true если здание может быть построено
    /// </summary>
    /// <param name="position">позиция строящегося здания на tilemap</param>
    public abstract bool IsCanBeBuild(Vector2Int position);

    /// <summary>
    /// Действие которое производит здание в конце хода
    /// </summary>
    public abstract void OnEndTurn();

    /// <summary>
    /// Действие выполняемое зданием сразу после строительства
    /// </summary>
    public abstract void OnBuilding();

    public abstract bool[,] GetAvailibleCells();

    public bool CheckPosssibilityBuilding(Vector2Int position)
    {
        bool[,] cells = GetAvailibleCells();
        return cells[position.x- levelManager.offset.x , position.y - levelManager.offset.y];
    }
}