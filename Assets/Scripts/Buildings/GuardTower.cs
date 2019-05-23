﻿using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
[Serializable]
public class GuardTower : GeneralBuilding
{
    public override TileBase GetTile()
    {
        return levelManager.GetTile(TileNames.GUARD_TOWER);
    }

    public override Sprite GetSprite()
    {
        return levelManager.GetSprite(TileNames.GUARD_TOWER);
    }

    public override Sprite GetSpriteIcon()
    {
        return levelManager.GetIconSprite(TileNames.GUARD_TOWER_ICON);
    }

    public override float GetPrice()
    {
        return Prices.GUARD_TOWER_PRICE;
    }

    public override string GetDescription()
    {
        return "Защищает вас от врагов";
    }

    public override bool IsCanBeBuild(Vector2Int position)
    {

        if (!(levelManager.resourceData.Resources.GetLength(0) > position.x) ||
            !(levelManager.resourceData.Resources.GetLength(1) > position.y))
        {
            return false;
        }
        if (levelManager.availibleBuildingData.IsAvailibleBuilding[position.x, position.y] == true)
        {
            return true;
        }

        return false;
    }

    public override void OnBuilding()
    {
        
    }

    public override void OnEndTurn()
    {
        throw new System.NotImplementedException();
    }

    public override bool[,] GetAvailibleCells()
    {
        return levelManager.availibleBuildingData.IsAvailibleBuilding;
       // bool[,] result = new bool[levelManager.worldSize.x, levelManager.worldSize.y];

    }
}