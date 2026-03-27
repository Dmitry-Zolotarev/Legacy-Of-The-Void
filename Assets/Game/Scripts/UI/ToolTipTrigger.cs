using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string tooltipText;
    private bool hasShown = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(tooltipText) && !hasShown)
        {
            ToolTip.Instance.transform.position = transform.position;
            ToolTip.Instance.ShowTooltip(tooltipText);
            hasShown = true;
        }     
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTip.Instance.HideTooltip();
        hasShown = false;
    }
}