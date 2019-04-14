using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private const float ANCHOR_MAX_Y=0.9f;
    public GameObject oil;
    public Sprite positiveChangeOilBarSprite;
    public Sprite negativeChangeOilBarSprite;
    public GameObject changeOil;
    private Image imageChangeOil;
    public void Start()
    {
        imageChangeOil = changeOil.GetComponent<Image>();
        RectTransform rect = oil.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(rect.anchorMin.x, rect.anchorMin.y);
        rect.anchorMax = new Vector2(rect.anchorMax.x, ANCHOR_MAX_Y * ResourceManager.START_OIL / 100);
        ChangeOilChangeBar(ResourceManager.EXPENSE_OIL);
        ResourceManager.OilChangeEvent += ChangeOilBar;
        ResourceManager.OilChangeEvent += ChangeOilChangeBar;
    }

    public  void ChangeOilBar(float percent)
    {
        RectTransform rect = oil.GetComponent<RectTransform>();
        rect.anchorMax=new Vector2(rect.anchorMax.x, rect.anchorMax.y+ ANCHOR_MAX_Y *percent/100);
    }

    public void ChangeOilChangeBar(float percent)
    {
        RectTransform rect = oil.GetComponent<RectTransform>();
        RectTransform rectChangeoil = changeOil.GetComponent<RectTransform>();
        if (percent >= 0)
        {
            imageChangeOil.sprite = positiveChangeOilBarSprite;
            rectChangeoil.anchorMin=new Vector2(rect.anchorMin.x,rect.anchorMax.y);
            rectChangeoil.anchorMax = new Vector2(rect.anchorMax.x, rect.anchorMax.y + ANCHOR_MAX_Y * percent / 100);
        }
        else
        {
            imageChangeOil.sprite = negativeChangeOilBarSprite;
            rectChangeoil.anchorMax = new Vector2(rect.anchorMax.x, rect.anchorMax.y);
            rectChangeoil.anchorMin = new Vector2(rect.anchorMin.x, rect.anchorMax.y + ANCHOR_MAX_Y * percent / 100);
        }
    }
}