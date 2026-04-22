using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class TravelSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI silverAmountLabel;
    [SerializeField] private TextMeshProUGUI rankLabel;
    [SerializeField] private TextMeshProUGUI stageNameLabel;
    [SerializeField] private TextMeshProUGUI LootedSilverLabel;

    [SerializeField] private TooltipTrigger rankPanel;
    
    [SerializeField] private GameObject ModalWindowsCanvas;

    [SerializeField] private CampaignStage[] campaignStages;
    [SerializeField] private TravelPanel lootPanel;
    [SerializeField] private TravelPanel fightPanel;
    [SerializeField] private TravelPanel restPanel;

    [HideInInspector] public int CurrentStage = 0;
    [HideInInspector] public int SilverBonus = 0;
    
    public static TravelSystem Instance;
    public GameObject TravelPanels;
    private Image background;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        background = GetComponent<Image>();
        ModalWindowsCanvas.SetActive(false);
        UpdateStage();
    }
    private void OnEnable()
    {
        UpdateLabels();
    }
    private void FixedUpdate()
    {
        UpdateLabels();
    }
    private void OnDisable()
    {
        ToolTip.Instance.HideTooltip();
    }
    private void UpdateLabels()
    {
        silverAmountLabel?.SetText(GameCore.Instance.Master.Silver.ToString());

        var rankName = GameCore.Instance.Ranks[GameCore.Instance.Master.CurrentRank].Name;

        rankPanel.tooltipText = "Ранг игрока:\n" + rankName;
        rankLabel?.SetText(rankName);
    }
    public void AddSilverToPlayer()
    {
        if (SilverBonus > 0)
        {
            GameCore.Instance.Master.Silver += SilverBonus;        
        }
    }
    public void ShowLootDialog()
    {
        ModalWindowsCanvas.SetActive(true);
        LootedSilverLabel.SetText(SilverBonus.ToString());
    }
    private bool NotNull(TravelAction action)
    {
        return action != null && !string.IsNullOrEmpty(action.HeaderText);
    } 
    public void UpdateStage()
    {
        if (CurrentStage < campaignStages.Length)
        {
            var stage = campaignStages[CurrentStage];

            fightPanel?.gameObject.SetActive(NotNull(stage.FightAction));
            fightPanel?.UpdateAction(stage.FightAction);

            lootPanel?.gameObject.SetActive(NotNull(stage.LootAction));
            lootPanel?.UpdateAction(stage.LootAction);

            restPanel?.gameObject.SetActive(NotNull(stage.RestAction));
            restPanel?.UpdateAction(stage.RestAction);

            if(stage.BackgroundSprite != null) background.sprite = stage.BackgroundSprite;
            CurrentStage++;
            stageNameLabel.SetText(stage.StageName);
        }   
    }
}
