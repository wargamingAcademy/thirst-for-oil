using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс отвечающий за графическое отображение нефти в колбе
/// </summary>
public class UIOilController : MonoBehaviour
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
        ResourceManager.OilChangeEvent += SetOilBarValue;
        ResourceManager.IncomeChangeEvent += SetIncomeOilBarValue;
        SetIncomeOilBarValue(ResourceManager.EXPENSE_OIL/ResourceManager.MAX_OIL);
        priceOil.SetActive(false);
    }

    /// <summary>
    /// Установить основной уровень нефти
    /// </summary>
    /// <param name="normalizedPercent">нормализованный процент от 0 до 1</param>
    public  void SetOilBarValue(float normalizedPercent)
    {
        oilBarRect = oil.GetComponent<RectTransform>();
        float oilMax = oilBarRect.anchorMax.y;
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
        if (min < oilMax)
        {
            difference = -difference;
        }
        SetIncomeOilBarValue(difference);

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

    /// <summary>
    /// Установить уровень прироста нефти
    /// </summary>
    /// <param name="normalizedPercent">нормализованный процент от 0 до 1</param>
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

    /// <summary>
    /// Установить уровень стоимости здания
    /// </summary>
    /// <param name="normalizedPercent">нормализованный процент от 0 до 1</param>
    public void ShowPriceBuildingOnBar(float normalizedPercent)
    {
        priceOil.SetActive(true);
        if (incomeOilBarRect.anchorMax.y > oilBarRect.anchorMax.y)
        {
            priceOilBarRect.anchorMax =new Vector2(oilBarRect.anchorMax.x,oilBarRect.anchorMax.y);
            if (oilBarRect.anchorMax.y - normalizedPercent> Constants.ANCHOR_MIN_Y)
            {
                priceOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x,
                    oilBarRect.anchorMax.y - normalizedPercent);
            }
            else
            {
                priceOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x, Constants.ANCHOR_MIN_Y);
            }
        }
        else
        {
            priceOilBarRect.anchorMax = new Vector2(oilBarRect.anchorMax.x, incomeOilBarRect.anchorMin.y);
            if (incomeOilBarRect.anchorMin.y - normalizedPercent > Constants.ANCHOR_MIN_Y)
            {
                priceOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x,
                    incomeOilBarRect.anchorMin.y - normalizedPercent);
            }
            else
            {
                priceOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x, Constants.ANCHOR_MIN_Y);
            }
        }
    }

    /// <summary>
    /// Скрыть уровень стоимости здания
    /// </summary>
    public void HidePriceBuildingOnBar()
    {
        priceOil.SetActive(false);
    }
}