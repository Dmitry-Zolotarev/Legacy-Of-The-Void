using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TravelSystem : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI SilverLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;
    [SerializeField] private TextMeshProUGUI BattleDescriptionLabel;
    [SerializeField] private TooltipTrigger RankPanel;
    [SerializeField] private GameObject ChonMaButton;
    [HideInInspector] public int SilverBonus = 0;
    [HideInInspector] public TravelToFightButton SelectedLevel;
    public BattleLaunchButton BattleLaunchButton;
    public GameObject TravelSystemCanvas;
    public GameObject TravelFightDialog;
    public GameObject TravelResultsWindow;
    public TextMeshProUGUI TravelResultText;
    public TextMeshProUGUI LootedSilverText;
    
    public Image EnemyImage;

    public static TravelSystem Instance;
    [SerializeField] private GameObject[] demonButtons;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        TravelSystemCanvas.SetActive(false);

        for(int i = 0; i < demonButtons.Length; i++)
        {
            demonButtons[i].SetActive(!GameCore.Instance.Enemies[i].IsDead);
        }
    }
    private void TryActivateChonMa()
    {
        bool allDemonsMurdered = true;
        
        for(int i = 0; i < 3; i++)
        {
            if (!GameCore.Instance.Enemies[i].IsDead) allDemonsMurdered = false;
        }
        ChonMaButton.gameObject.SetActive(allDemonsMurdered);
    }
    public void OnEnable()
    {
        TravelFightDialog.SetActive(false);
        TravelResultsWindow.SetActive(false);
        TravelSystemCanvas.SetActive(false);
        TryActivateChonMa();
        UpdateLabels();
    }
    public void ShowFightDialog(string battleDescription)
    {
        BattleDescriptionLabel.SetText(battleDescription);
        TravelSystemCanvas.SetActive(true);
        TravelResultsWindow.SetActive(false);
        TravelFightDialog.SetActive(true);
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
