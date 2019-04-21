using UnityEngine;


public class ResourceManager:MonoBehaviour
{
    public static ResourceManager Instance => instance;
    static ResourceManager instance;

    [SerializeField]
    private UIController uiController;
    public const float EXPENSE_OIL = -10f;
    public const float START_OIL = 50f;
    public const float MAX_OIL = 100f;
    private float incomeOil;
    public delegate void MethodContainer(float oil);
    public static event MethodContainer OilChangeEvent;
    public static event MethodContainer IncomeChangeEvent;
    public delegate void OnLoseDelegate();
    public static event OnLoseDelegate LoseEvent;
    private float oil;
    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple ResourceManager");
            Destroy(this);
            return;
        }
        instance = this;
    }
    void Start()
    {
      
        TurnController.TurnEndEvent += RecalculateResources;
        Oil = START_OIL;
        IncomeOil = EXPENSE_OIL;
        //OilChangeEvent(oil);
    }

    public void RecalculateResources()
    {
        
        Oil += IncomeOil;        
        OilChangeEvent(IncomeOil);
        if (Oil <= 0f)
        {
          //  LoseEvent();
        }
    }

    public float IncomeOil
    {
        get { return incomeOil; }
        set
        {
            incomeOil = value;
            IncomeChangeEvent(incomeOil/MAX_OIL);
        }
    }
    public float Oil
    {
        get { return oil;}
        set
        {           
           /* uiController.SetOilBarValue(-Prices.GUARD_TOWER);
            uiController.ChangeOilChangeBar(resourceManager.expenseOil);
            uiController.ShowPriceBuildingOnBar(Prices.GUARD_TOWER);*/
            if (value < 0)
            {
                oil = 0;
            }

            if (value >MAX_OIL)
            {
                oil = MAX_OIL;
            }

            oil = value;
            OilChangeEvent(oil/MAX_OIL);
        }

    }
}