using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;

public class BuildingPool : ObjectPool<BuildingPool, BuildingObject>
{
    static protected Dictionary<GameObject, BuildingPool> s_PoolInstances = new Dictionary<GameObject, BuildingPool>();


    private void Awake()
    {
        if (prefab != null && !s_PoolInstances.ContainsKey(prefab))
            s_PoolInstances.Add(prefab, this);
    }

    private void OnDestroy()
    {
        s_PoolInstances.Remove(prefab);
    }

    static public BuildingPool GetObjectPool(GameObject prefab, int initialPoolCount = 10)
    {
        BuildingPool objPool = null;
        if (!s_PoolInstances.TryGetValue(prefab, out objPool))
        {
            GameObject obj = new GameObject(prefab.name + "_Pool");
            objPool = obj.AddComponent<BuildingPool>();
            obj.AddComponent<Canvas>();
            obj.AddComponent<GraphicRaycaster>();
            Canvas canvas = obj.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 100;
            objPool.prefab = prefab;
            objPool.initialPoolCount = initialPoolCount;

            s_PoolInstances[prefab] = objPool;
        }

        return objPool;
    }
}

public class BuildingObject : PoolObject<BuildingPool, BuildingObject>
{
    public Transform transform;
    public Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;

    protected override void SetReferences()
    {
        transform = instance.transform;
        rigidbody2D = instance.GetComponent<Rigidbody2D>();
        spriteRenderer = instance.GetComponent<SpriteRenderer>();
       /* obstacle = instance.GetComponent<BuildingObject>();
        obstacle.obstaclePoolObject = this;*/
    }

    public override void WakeUp()
    {
       // transform.position = position;
        instance.SetActive(true);
    }

    public override void Sleep()
    {
        instance.SetActive(false);
    }
}