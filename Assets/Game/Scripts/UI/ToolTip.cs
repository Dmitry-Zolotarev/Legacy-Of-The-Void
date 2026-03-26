using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    [SerializeField] private GameObject tooltipObject;
    [SerializeField] private TextMeshProUGUI tooltipText;

    public static ToolTip Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void ShowTooltip(string text)
    {
        tooltipObject.SetActive(true);
        tooltipText.text = text;
    }
    public void HideTooltip()
    {
        tooltipObject.SetActive(false);
    }
}