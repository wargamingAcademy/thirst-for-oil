using Gamekit2D;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

public class InterfaceInteraction : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerInput.Instance.CellSelected.Disable();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerInput.Instance.CellSelected.Enable();
    }
}