using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class TravelSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SilverLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;
    [SerializeField] private TooltipTrigger RankPanel;
    [SerializeField] private TextMeshProUGUI StageNameLabel;

    [SerializeField] private CampaignStage[] campaignStages;
    [SerializeField] private TravelPanel fightPanel;
    [SerializeField] private TravelPanel lootPanel;
    [SerializeField] private TravelPanel restPanel;

    [HideInInspector] public int CurrentStage = 0;
    [HideInInspector] public int SilverBonus = 0;
    

    public static TravelSystem Instance;
    private Image background;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        background = GetComponent<Image>();
        UpdateStage();
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
        UpdateStage();
    }
    public void UpdateStage()
    {
        if (CurrentStage < campaignStages.Length)
        {
            var stage = campaignStages[CurrentStage];

            fightPanel?.gameObject.SetActive(stage.FightAction != null);
            fightPanel?.UpdateAction(stage.FightAction);

            lootPanel?.gameObject.SetActive(stage.LootAction != null);
            lootPanel?.UpdateAction(stage.LootAction);

            restPanel?.gameObject.SetActive(stage.RestAction != null);
            restPanel?.UpdateAction(stage.RestAction);

            if(stage.BackgroundSprite != null) background.sprite = stage.BackgroundSprite;
            CurrentStage++;
            UpdateLabels();
            StageNameLabel.SetText(stage.StageName);
        }   
    }
}
