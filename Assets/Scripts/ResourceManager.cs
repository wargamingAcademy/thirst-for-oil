using UnityEngine;


public class ResourceManager:MonoBehaviour
{
    public const float EXPENSE_OIL = 10f;
    public const float START_OIL = 50f;
    public const float END_OIL = 100f;
    public delegate void MethodContainer(float oil);
    public static event MethodContainer OilChangeEvent;
    public delegate void OnLoseDelegate();
    public static event OnLoseDelegate LoseEvent;
    void Start()
    {
        Oil = START_OIL;
        TurnController.TurnEndEvent += RecalculateResources;
        //OilChangeEvent(oil);
    }

    public void RecalculateResources()
    {
        Oil -= EXPENSE_OIL;        
        OilChangeEvent(EXPENSE_OIL);
        if (Oil <= 0f)
        {
            LoseEvent();
        }
    }

    public float Oil { get; set; }
}