using System.Collections.Generic;
using UnityEngine;

public class ModificatorManager : MonoBehaviour
{
    private List<IResourceModificator> resourceModificators;

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
        resourceModificators=new List<IResourceModificator>();
    }

    public void RegisterResourceModificator(IResourceModificator resModificator)
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
   public IResourceModificator GetResourceModificator(IResourceModificator modificator)
    {
        foreach (var resourceModificator in resourceModificators)
        {
            if (modificator.GetType() == resourceModificator.GetType())
            {
                return resourceModificator;
            }
        }
        return null;
    }
}
