using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tooltipText;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float fontSize = 16f;
    private bool hasShown = false;
    
    public void OnPointerEnter(PointerEventData eventData) => ShowTooltip(tooltipText);
    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTip.Instance.HideTooltip();
        hasShown = false;
    }
    public void ShowTooltip(string text)
    {
        
        if (hasShown || string.IsNullOrEmpty(text)) return;

        ToolTip.Instance.transform.position = transform.position + offset;
        ToolTip.Instance.SetFontSize(fontSize);
        ToolTip.Instance.ShowTooltip(text);
        hasShown = true;
    }
}