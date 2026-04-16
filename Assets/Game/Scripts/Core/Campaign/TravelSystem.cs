using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TravelSystem : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI SilverLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;
    [SerializeField] private TooltipTrigger RankPanel;
    [HideInInspector] public int SilverBonus = 0;
    
    public static TravelSystem Instance;
    [SerializeField] private GameObject[] demonButtons;


    private void Awake()
    {
        if (Instance == null) Instance = this;

        for(int i = 0; i < demonButtons.Length; i++)
        {
            demonButtons[i].SetActive(!GameCore.Instance.Enemies[i].IsDead);
        }
    }
    private void OnEnable()
    {
        UpdateLabels();
    }
    private void OnDisable()
    {
        ToolTip.Instance.HideTooltip();
    }
    private void UpdateLabels()
    {
        SilverLabel?.SetText(GameCore.Instance.Master.Silver.ToString());

        var rankName = GameCore.Instance.Ranks[GameCore.Instance.Master.CurrentRank].Name;

        RankPanel.tooltipText = "Ранг игрока:\n" + rankName;
        RankLabel?.SetText(rankName);
    }
    public void AddSilverToPlayer()
    {
        GameCore.Instance.Master.Silver += SilverBonus;
        UpdateLabels();
    }
}
