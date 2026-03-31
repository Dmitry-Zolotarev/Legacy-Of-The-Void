using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainHubUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SilverAmountLabel;

    [SerializeField] private Image MasterSprite;
    [SerializeField] private Sprite YoungMasterSprite;
    [SerializeField] private Sprite AdultMasterSprite;
    [SerializeField] private Sprite OldMasterSprite;
    [SerializeField] private StatsPanel MiniStatsPanel;
    [SerializeField] private GameObject AgeNotification;
    [SerializeField] private TextMeshProUGUI RankLabel;
    [SerializeField] private TooltipTrigger RankPanel;
    [SerializeField] private int BecomeAdultAge = 25;
    [SerializeField] private int NotificationAge = 40;
    [SerializeField] private int BecomeOldAge = 60;
    public static MainHubUI Instance;
    private CharacterData master;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start()
    {
        RefreshUI();                   
    }
    private void FixedUpdate()
    {
        RefreshUI();
    }
    public void RefreshUI()
    {
        if (GameCore.Instance == null || GameCore.Instance.Master == null) return;
        master = GameCore.Instance.Master;

        AgeNotification.SetActive(master != null && master.Age >= NotificationAge && master.CurrentRank < (int)master.RankForBecomeTeacher);

        UpdateLabels();
        UpdateMasterSprite();
    }
    public void UpdateMasterSprite()
    {
        if (master == null || MasterSprite == null) return;    

        if (master.Age >= BecomeOldAge)
        {
            MasterSprite.sprite = OldMasterSprite;
        }
        else if (master.Age >= BecomeAdultAge)
        {
            MasterSprite.sprite = AdultMasterSprite;
        }
        else MasterSprite.sprite = YoungMasterSprite;
    }
    private void UpdateLabels()
    {
        if (master == null) return;  
        MiniStatsPanel.UpdateLabels();
        SilverAmountLabel?.SetText(master.Silver.ToString());

        var rankName = GameCore.Instance.Ranks[GameCore.Instance.Master.CurrentRank].Name;
        RankPanel.tooltipText = "Ранг игрока:\n" + rankName;
        RankLabel?.SetText(rankName);
    }
}