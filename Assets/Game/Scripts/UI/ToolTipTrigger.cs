using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string tooltipText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!string.IsNullOrEmpty(tooltipText)) ToolTip.Instance.ShowTooltip(tooltipText);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTip.Instance.HideTooltip();
    }
}