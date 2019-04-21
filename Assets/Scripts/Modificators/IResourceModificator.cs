using UnityEngine;
using UnityEditor;

public interface IResourceModificator
{
    GeneralBuilding GetBuilding();
    void Initialize();
}