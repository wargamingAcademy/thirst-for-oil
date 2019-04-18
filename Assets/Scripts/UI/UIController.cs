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
    private RectTransform changeOilBarRect;
    private RectTransform priceOilBarRect;
    public void Start()
    {
        imageChangeOil = changeOil.GetComponent<Image>();
        Image priceOilImage = priceOil.GetComponent<Image>();
        priceOilImage.sprite = priceOilBarSprite;
        oilBarRect = oil.GetComponent<RectTransform>();
        changeOilBarRect = changeOil.GetComponent<RectTransform>();
        priceOilBarRect = priceOil.GetComponent<RectTransform>();
        oilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x, oilBarRect.anchorMin.y);
        oilBarRect.anchorMax = new Vector2(oilBarRect.anchorMax.x, Constants.ANCHOR_MAX_Y * ResourceManager.START_OIL / 100);
        ChangeOilChangeBar(ResourceManager.EXPENSE_OIL);
        ResourceManager.OilChangeEvent += ChangeOilBar;
        ResourceManager.OilChangeEvent += ChangeOilChangeBar;
        priceOil.SetActive(false);
    }

    public  void ChangeOilBar(float percent)
    {
        oilBarRect = oil.GetComponent<RectTransform>();
        if (oilBarRect.anchorMax.y + Constants.ANCHOR_MAX_Y * percent / 100 > Constants.ANCHOR_MAX_Y)
        {
            oilBarRect.anchorMax =
                new Vector2(oilBarRect.anchorMax.x, Constants.ANCHOR_MAX_Y);
        }
        else
        {
            oilBarRect.anchorMax =
                new Vector2(oilBarRect.anchorMax.x, oilBarRect.anchorMax.y + Constants.ANCHOR_MAX_Y * percent / 100);
        }

        if (changeOilBarRect.anchorMax.y + Constants.ANCHOR_MAX_Y * percent / 100 > Constants.ANCHOR_MAX_Y)
        {
            changeOilBarRect.anchorMax = new Vector2(changeOilBarRect.anchorMax.x, Constants.ANCHOR_MAX_Y);
        }
        else
        {
            changeOilBarRect.anchorMax = new Vector2(changeOilBarRect.anchorMax.x, changeOilBarRect.anchorMax.y + Constants.ANCHOR_MAX_Y * percent / 100);
        }
       
        changeOilBarRect.anchorMin = new Vector2(changeOilBarRect.anchorMin.x, changeOilBarRect.anchorMin.y + Constants.ANCHOR_MAX_Y * percent / 100);
        priceOilBarRect.anchorMax = new Vector2(priceOilBarRect.anchorMax.x, priceOilBarRect.anchorMax.y + Constants.ANCHOR_MAX_Y * percent / 100);
        priceOilBarRect.anchorMin = new Vector2(priceOilBarRect.anchorMin.x, priceOilBarRect.anchorMin.y + Constants.ANCHOR_MAX_Y * percent / 100);
    }

    public void ChangeOilChangeBar(float percent)
    {
        changeOilBarRect = changeOil.GetComponent<RectTransform>();
        if (percent >= 0)
        {
            imageChangeOil.sprite = positiveChangeOilBarSprite;
            if (oilBarRect.anchorMax.y < Constants.ANCHOR_MIN_Y)
            {
                changeOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x, Constants.ANCHOR_MIN_Y);
            }
            else
            {
                changeOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x, oilBarRect.anchorMax.y);
            }
            
            if (oilBarRect.anchorMax.y + Constants.ANCHOR_MAX_Y * percent / 100 > Constants.ANCHOR_MAX_Y)
            {
                changeOilBarRect.anchorMax = new Vector2(oilBarRect.anchorMax.x, Constants.ANCHOR_MAX_Y);
            }
            else
            {
                changeOilBarRect.anchorMax = new Vector2(oilBarRect.anchorMax.x, oilBarRect.anchorMax.y + Constants.ANCHOR_MAX_Y * percent / 100);
            }

            
        }
        else
        {
            imageChangeOil.sprite = negativeChangeOilBarSprite;
            changeOilBarRect.anchorMax = new Vector2(oilBarRect.anchorMax.x, oilBarRect.anchorMax.y);
            if (oilBarRect.anchorMax.y + Constants.ANCHOR_MAX_Y * percent / 100 < Constants.ANCHOR_MIN_Y)
            {
                changeOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x, Constants.ANCHOR_MIN_Y);
            }
            else
            {
                changeOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x,
                    oilBarRect.anchorMax.y + Constants.ANCHOR_MAX_Y * percent / 100);
            }
        }
    }

    public void ShowPriceBuildingOnBar(float percent)
    {
        priceOil.SetActive(true);
        if (changeOilBarRect.anchorMax.y > oilBarRect.anchorMax.y)
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
            priceOilBarRect.anchorMax = new Vector2(oilBarRect.anchorMax.x, changeOilBarRect.anchorMin.y);
            if (changeOilBarRect.anchorMin.y - Constants.ANCHOR_MAX_Y * percent / 100 > Constants.ANCHOR_MIN_Y)
            {
                priceOilBarRect.anchorMin = new Vector2(oilBarRect.anchorMin.x,
                    changeOilBarRect.anchorMin.y - Constants.ANCHOR_MAX_Y * percent / 100);
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