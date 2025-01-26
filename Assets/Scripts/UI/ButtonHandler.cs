using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData) {
        MonitorController.Instance.EnableTooltip(GetComponent<ButtonStats>().tooltipDisplay);
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        MonitorController.Instance.DisableTooltip();
    }

    public void OnPointerClick(PointerEventData pointerEventData) {
        MonitorController.Instance.DisableTooltip();
    }
}
