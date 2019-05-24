using UnityEngine;

/// <summary>
/// Класс отвечающий за управление ресурсами (пока только нефтью)
/// </summary>
public class ResourceManager:MonoBehaviour
{
    public static ResourceManager Instance => instance;
    static ResourceManager instance;

    [SerializeField]
    private UIOilController uiController;
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
    }

    public void RecalculateResources()
    {
        
        Oil += IncomeOil;        
        OilChangeEvent(Oil*Constants.ANCHOR_MAX_Y/100);
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