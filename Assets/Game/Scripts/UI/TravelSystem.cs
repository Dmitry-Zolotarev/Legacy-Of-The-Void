using UnityEngine;
using TMPro;

public class TravelSystem : MonoBehaviour
{
    
    [SerializeField] private int MinYears = 1;
    [SerializeField] private int MaxYears = 3;
    [SerializeField] private TextMeshProUGUI SilverLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;
    [SerializeField] private TextMeshProUGUI BattleDescriptionLabel;
    [HideInInspector] public int SilverBonus = 0;
    [HideInInspector] public TravelToFightButton SelectedLevel;
    public BattleLaunchButton BattleLaunchButton;
    public GameObject TravelSystemCanvas;
    public static TravelSystem Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        TravelSystemCanvas.SetActive(false);
    }
    public void OnEnable()
    {
        
        UpdateLabels();
    }
    public void ShowFightDialog(string battleDescription)
    {
        BattleDescriptionLabel.SetText(battleDescription);
        TravelSystemCanvas.SetActive(true);       
    }
    private void UpdateLabels()
    {
        SilverLabel?.SetText(GameCore.Instance.Master.Silver.ToString());
        var rankName = GameCore.Instance.Ranks[GameCore.Instance.Master.CurrentRank].Name;
        RankLabel?.SetText("Ранг: " + rankName.ToLower());
    }
    public void AddSilverToPlayer()
    {
        SelectedLevel.enemyDefeated = true;
        GameCore.Instance.Master.Silver += SilverBonus;
    }
}
