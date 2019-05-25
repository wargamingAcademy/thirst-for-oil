using Assets.Scripts.Modificators;
using System.Collections.Generic;
using UnityEngine;

public class ModificatorManager : MonoBehaviour
{
    private List<IModificator> resourceModificators;

    public static ModificatorManager Instance => instance;

    static ModificatorManager instance;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple ModificatorManager");
            Destroy(this);
            return;
        }
        instance = this;
        resourceModificators=new List<IModificator>();
    }

    public void RegisterResourceModificator(IModificator resModificator)
    {
        foreach (var resourceModificator in resourceModificators)
        {
            if (resModificator.GetType() == resourceModificator.GetType())
            {
                return;
            }
        }
        resModificator.Initialize();
        resourceModificators.Add(resModificator);
    }

   public IModificator GetResourceModificator(IModificator modificator)
    {
        foreach (var resourceModificator in resourceModificators)
        {
            if (modificator.GetType() == resourceModificator.GetType())
            {
                return resourceModificator;
            }
        }
        modificator.Initialize();
        resourceModificators.Add(modificator);
        return modificator;
    }
}
