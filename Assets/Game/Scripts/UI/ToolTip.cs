using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private GameObject toolTipImage;
    public static ToolTip Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void ShowTooltip(string text)
    {
        toolTipImage.SetActive(true);
        tooltipText.text = text;
    }
    public void HideTooltip()
    {
        toolTipImage.SetActive(false);
    }
    public void SetFontSize(float value) => tooltipText.fontSize = value;
}