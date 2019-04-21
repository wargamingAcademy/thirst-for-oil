using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
   
    [SerializeField]
    private GameObject oil;
    [SerializeField]
    private Sprite positiveChangeOilBarSprite;
    [SerializeField]
    private Sprite negativeChangeOilBarSprite;
    [SerializeField]
    private Sprite priceOilBarSprite;
    [SerializeField]
    private GameObject changeOil;
    [SerializeField]
    private GameObject priceOil;
    private Image imageChangeOil;
    private RectTransform oilBarRect;
    private RectTransform incomeOilBarRect;
    private RectTransform priceOilBarRect;
    public void Awake()
    {
        imageChangeOil = changeOil.GetComponent<Image>();
        Image priceOilImage = priceOil.GetComponent<Image>();
        priceOilImage.sprite = priceOilBarSprite;
        oilBarRect = oil.GetComponent<RectTransform>();
        incomeOilBarRect = changeOil.GetComponent<RectTransform>();
        priceOilBarRect = priceOil.GetComponent<RectTransform>();
        oilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x, oilBarRect.anchorMin.y);
      //  oilBarRect.anchorMax = new Vector2(oilBarRect.anchorMax.x, Constants.ANCHOR_MAX_Y * ResourceManager.START_OIL / 100);
        ResourceManager.OilChangeEvent += SetOilBarValue;
        ResourceManager.OilChangeEvent += SetIncomeOilBarValue;
        ResourceManager.IncomeChangeEvent += SetIncomeOilBarValue;
        SetIncomeOilBarValue(ResourceManager.EXPENSE_OIL/ResourceManager.MAX_OIL);
        priceOil.SetActive(false);
    }

    public  void SetOilBarValue(float normalizedPercent)
    {
        oilBarRect = oil.GetComponent<RectTransform>();
        if (Constants.ANCHOR_MAX_Y * normalizedPercent > Constants.ANCHOR_MAX_Y)
        {
            oilBarRect.anchorMax =
                new Vector2(oilBarRect.anchorMax.x, Constants.ANCHOR_MAX_Y);
        }
        else
        {
            oilBarRect.anchorMax =
                new Vector2(oilBarRect.anchorMax.x, Constants.ANCHOR_MAX_Y * normalizedPercent);
        }

        float min = incomeOilBarRect.anchorMin.y;
        float max= incomeOilBarRect.anchorMax.y;
        float difference = max - min;
        if (difference > 0)
        {
            if (oilBarRect.anchorMax.y + difference > Constants.ANCHOR_MAX_Y)
            {
                incomeOilBarRect.anchorMax = new Vector2(incomeOilBarRect.anchorMax.x, Constants.ANCHOR_MAX_Y);
            }
            else
            {
                incomeOilBarRect.anchorMax = new Vector2(incomeOilBarRect.anchorMax.x, oilBarRect.anchorMax.y + difference);
            }
            incomeOilBarRect.anchorMin = oilBarRect.anchorMax;
        }
        else
        {
            incomeOilBarRect.anchorMax = oilBarRect.anchorMax;
            incomeOilBarRect.anchorMin = new Vector2(incomeOilBarRect.anchorMin.x, incomeOilBarRect.anchorMin.y + difference);
        }
        min = priceOilBarRect.anchorMin.y;
        max = priceOilBarRect.anchorMax.y;
        difference = max - min;
        priceOilBarRect.anchorMax = new Vector2(priceOilBarRect.anchorMax.x, incomeOilBarRect.anchorMin.y);
        if (incomeOilBarRect.anchorMin.y - difference < Constants.ANCHOR_MIN_Y)
        {
            priceOilBarRect.anchorMin = new Vector2(priceOilBarRect.anchorMin.x, Constants.ANCHOR_MIN_Y);
        }
        else
        {
            priceOilBarRect.anchorMin = new Vector2(priceOilBarRect.anchorMin.x, incomeOilBarRect.anchorMin.y - difference);
        }
       
    }

    public void SetIncomeOilBarValue(float normalizedPercent)
    {
        incomeOilBarRect = changeOil.GetComponent<RectTransform>();
        if (normalizedPercent >= 0)
        {
            imageChangeOil.sprite = positiveChangeOilBarSprite;
            incomeOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x, oilBarRect.anchorMax.y);
            
            if (oilBarRect.anchorMax.y + normalizedPercent > Constants.ANCHOR_MAX_Y)
            {
                incomeOilBarRect.anchorMax = new Vector2(oilBarRect.anchorMax.x, Constants.ANCHOR_MAX_Y);
            }
            else
            {
                incomeOilBarRect.anchorMax = new Vector2(oilBarRect.anchorMax.x, oilBarRect.anchorMax.y +  normalizedPercent);
            }
        }
        else
        {
            imageChangeOil.sprite = negativeChangeOilBarSprite;
            incomeOilBarRect.anchorMax = new Vector2(oilBarRect.anchorMax.x, oilBarRect.anchorMax.y);
            if (oilBarRect.anchorMax.y + normalizedPercent< Constants.ANCHOR_MIN_Y)
            {
                incomeOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x, Constants.ANCHOR_MIN_Y);
            }
            else
            {
                incomeOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x,
                    oilBarRect.anchorMax.y + normalizedPercent);
            }
        }
    }

    public void ShowPriceBuildingOnBar(float percent)
    {
        priceOil.SetActive(true);
        if (incomeOilBarRect.anchorMax.y > oilBarRect.anchorMax.y)
        {
            priceOilBarRect.anchorMax =new Vector2(oilBarRect.anchorMax.x,oilBarRect.anchorMax.y);
            if (oilBarRect.anchorMax.y - Constants.ANCHOR_MAX_Y * percent / 100 > Constants.ANCHOR_MIN_Y)
            {
                priceOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x,
                    oilBarRect.anchorMax.y - Constants.ANCHOR_MAX_Y * percent / 100);
            }
            else
            {
                priceOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x, Constants.ANCHOR_MIN_Y);
            }
        }
        else
        {
            priceOilBarRect.anchorMax = new Vector2(oilBarRect.anchorMax.x, incomeOilBarRect.anchorMin.y);
            if (incomeOilBarRect.anchorMin.y - Constants.ANCHOR_MAX_Y * percent / 100 > Constants.ANCHOR_MIN_Y)
            {
                priceOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x,
                    incomeOilBarRect.anchorMin.y - Constants.ANCHOR_MAX_Y * percent / 100);
            }
            else
            {
                priceOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x, Constants.ANCHOR_MIN_Y);
            }
        }
    }

    public void HidePriceBuildingOnBar()
    {
        priceOil.SetActive(false);
    }
}