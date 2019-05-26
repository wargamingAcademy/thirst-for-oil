using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class Tooltip:MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private const float GUIDANCE_TIME = 0.3f;
        private float guidanceTime;
        private bool isMouseOVerButton = false;
        private bool tooltipActive = false;
        public GameObject tooltipCanvas;
        private GameObject tooltip;
        private bool isEnabled=true;
        public GeneralBuilding buiding;
        public void Awake()
        {
            if (tooltip == null)
            {
                tooltip = GameObject.Instantiate(tooltipCanvas);
                tooltip.SetActive(false);
            }          
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            isMouseOVerButton = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isMouseOVerButton = false;
        }

        public void Disable()
        {
            isEnabled = false;
        }

        public void Enable()
        {
            isEnabled = true;
        }
        public void Update()
        {
            if (isMouseOVerButton)
            {
                guidanceTime += Time.deltaTime;
                if ((guidanceTime >= GUIDANCE_TIME)&&(!tooltipActive)&&(isEnabled))
                {
                    tooltipActive = true;
                    tooltip.SetActive(true);

                    Transform buildingInfo = tooltip.transform.GetChild(0);
                    RectTransform rect = buildingInfo.gameObject.GetComponent<RectTransform>();

                    RectTransform tooltipRect = buildingInfo.gameObject.GetComponent<RectTransform>();
                   // Transform childButton = this.gameObject.transform.GetChild(0);
                   Transform parentObj = gameObject.transform.parent;
                    RectTransform childButtonRect = parentObj.GetComponent<RectTransform>();
                   // RectTransform promRect=new RectTransform();
                   float startX = childButtonRect.anchorMin.x * Screen.width;
                   float startY = childButtonRect.anchorMin.y * Screen.height;
                   float width = childButtonRect.sizeDelta.x;
                   Transform header = buildingInfo.GetChild(0);
                   header.GetComponent<TextMeshProUGUI>().text= buiding.GetName();
                   Transform description = buildingInfo.GetChild(1);
                    description.GetComponent<TextMeshProUGUI>().text=buiding.GetDescription();
                    Transform price = buildingInfo.GetChild(3);
                    price.GetComponent<TextMeshProUGUI>().text = buiding.GetPrice().ToString();
                    tooltipRect.position=new Vector2(startX + rect.rect.width / 2-width/2-10, startY + rect.rect.height / 2+width/2 + 30);
                   // newRect.position = new Vector2(this.transform.position.x + rect.rect.width / 2, this.transform.position.y + rect.rect.height / 2 + 30);
                    /* Rect promRect =new Rect(new Vector3(this.transform.position.x + rect.rect.width / 2, this.transform.position.y + rect.rect.height / 2 + 30)
                         ,new Vector2(newRect.rect.width,newRect.rect.height));
                     newRect.rect = promRect;
                         new Vector3(this.transform.position.x+rect.rect.width/2, this.transform.position.y + rect.rect.height / 2+30, this.transform.position.z); */
                }               
            }
            else
            {
                tooltipActive = false;
                guidanceTime = 0;
                tooltip.SetActive(false);
            }
        }
    }
}
