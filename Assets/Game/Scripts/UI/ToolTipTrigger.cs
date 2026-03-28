using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tooltipText;
    [SerializeField] private Vector3 offset = Vector3.zero;
    private bool hasShown = false;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(tooltipText) && !hasShown)
        {
            ToolTip.Instance.transform.position = transform.position + offset;
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