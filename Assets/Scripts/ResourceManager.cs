using UnityEngine;


public class ResourceManager:MonoBehaviour
{
    [SerializeField]
   private UIController uiController;
    public const float EXPENSE_OIL = -10f;
    public const float START_OIL = 50f;
    public const float END_OIL = 100f;
    public float expenseOil;
    public delegate void MethodContainer(float oil);
    public static event MethodContainer OilChangeEvent;
    public delegate void OnLoseDelegate();
    public static event OnLoseDelegate LoseEvent;
    private float oil;
    void Start()
    {
        Oil = START_OIL;
        expenseOil = EXPENSE_OIL;
        TurnController.TurnEndEvent += RecalculateResources;
        //OilChangeEvent(oil);
    }

    public void RecalculateResources()
    {
        
        Oil += expenseOil;        
        OilChangeEvent(expenseOil);
        if (Oil <= 0f)
        {
          //  LoseEvent();
        }
    }
    public float Oil
    {
        get { return oil;}
        set
        {
            if (value < 0)
            {
                oil = 0;
            }

            if (value > Constants.ANCHOR_MAX_Y)
            {
                oil = Constants.ANCHOR_MAX_Y * 10;
            }

            oil = value;
        }

    }
}