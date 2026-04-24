using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TravelSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI silverAmountLabel;
    [SerializeField] private TextMeshProUGUI stageNameLabel;

    [SerializeField] private TravelPanel lootPanel;
    [SerializeField] private TravelPanel fightPanel;
    [SerializeField] private TravelPanel restPanel;
    [SerializeField] private Image background;

    
    [HideInInspector] public int SilverBonus = 0;
    [HideInInspector] public RankItem LootItem;

    public GameObject ModalWindowsCanvas;
    public static TravelSystem Instance;    
    public TextMeshProUGUI LootLabel;
    public GameObject TravelPanels;

    [SerializeField] private GameObject finalVoidBreakCanvas;
    [SerializeField] private GameObject[] modalWindows;
    [SerializeField] private CampaignStage[] campaignStages;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
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
    public void OpenWindow(int windowID)
    {
        for (int i = 0; i < modalWindows.Length; i++) modalWindows[i]?.SetActive(i == windowID);
    }
    private void UpdateLabels()
    {
        try
        {
            silverAmountLabel?.SetText(GameCore.Instance.Master.Silver.ToString());
            var backroundSprite = campaignStages[GameCore.Instance.CurrentStage - 1].BackgroundSprite;
            if (backroundSprite != null) background.sprite = backroundSprite;
        }
        catch { }      
    }
    public void PickLoot()
    {
        if (SilverBonus > 0)
        {
            GameCore.Instance.Master.Silver += SilverBonus;        
        }
        if (LootItem != null)
        {
            GameCore.Instance.RankItems[(int)LootItem.ID].Count += LootItem.Count;
        }
    }
    private bool NotNull(TravelAction action)
    {
        return action != null && !string.IsNullOrEmpty(action.HeaderText);
    } 
    public void UpdateStage()
    {
        if (GameCore.Instance.CurrentStage < campaignStages.Length)
        {
            if (GameCore.Instance.CurrentStage >= campaignStages.Length) 
            {
                finalVoidBreakCanvas?.SetActive(true);
                return;
            }            
            var stage = campaignStages[GameCore.Instance.CurrentStage];

            fightPanel?.gameObject.SetActive(NotNull(stage.FightAction));
            fightPanel?.UpdateAction(stage.FightAction);

            lootPanel?.gameObject.SetActive(NotNull(stage.LootAction));
            lootPanel?.UpdateAction(stage.LootAction);

            restPanel?.gameObject.SetActive(NotNull(stage.RestAction));
            restPanel?.UpdateAction(stage.RestAction);

            stageNameLabel.SetText(stage.StageName);

            GameCore.Instance.CurrentStage++;
            
        }   
    }
}
