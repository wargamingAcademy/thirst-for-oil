using UnityEngine;


public class ResourceManager:MonoBehaviour
{
    public const float EXPENSE_OIL = -10f;
    public const float START_OIL = 50f;
    public const float END_OIL = 100f;
    private float oil;
    public delegate void MethodContainer(float oil);
    public static event MethodContainer OilChangeEvent;
    public delegate void OnLoseDelegat();
    public static event OnLoseDelegat LoseEvent;
    void Start()
    {
        oil = START_OIL;
        TurnController.TurnEndEvent += RecalculateResources;
        //OilChangeEvent(oil);
    }

    public void RecalculateResources()
    {
        oil -= EXPENSE_OIL;        
        OilChangeEvent(EXPENSE_OIL);
        if (oil <= 0f)
        {
            LoseEvent();
        }
    }
}