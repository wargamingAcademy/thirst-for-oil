using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Buildings
{
    public class BuildingManager:MonoBehaviour
    {
        private List<GeneralBuilding> buildings;
        private LevelManager levelManager;
        public void Awake()
        {
            buildings=new List<GeneralBuilding>();
            levelManager = FindObjectOfType<LevelManager>();
        }

        public void AddBuilding(GeneralBuilding building)
        {
            buildings.Add(building);
        }

        public void DestroyRandomBuilding()
        {
            if (buildings.Count == 0)
            {
                return;
            }
           float rnd= Random.Range(0, buildings.Count);
           int rndInt = Mathf.RoundToInt(rnd);          
           levelManager.buildingTilemap.SetTile(new Vector3Int(buildings[rndInt].Position.x, buildings[rndInt].Position.y, 0), null);
           buildings.RemoveAt(rndInt);
        }

        public int GetCountsBuildings(GeneralBuilding building)
        {
            int count = 0;
            foreach (var construction in buildings)
            {
                if (construction.GetType() == building.GetType())
                {
                    count++;
                }
            }
            return count;
        }
    }
}
