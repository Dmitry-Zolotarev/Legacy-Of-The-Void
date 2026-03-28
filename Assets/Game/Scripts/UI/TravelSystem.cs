using UnityEngine;
using TMPro;

public class TravelSystem : MonoBehaviour
{
    
    [SerializeField] private int MinYears = 1;
    [SerializeField] private int MaxYears = 3;
    [SerializeField] private TextMeshProUGUI SilverLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;
    [HideInInspector] public int LootedSilver = 0;
    public GameObject TravelSystemCanvas;
    public static TravelSystem Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void OnEnable()
    {
        TravelSystemCanvas.SetActive(false);
        UpdateLabels();
    }
    public void Travel()
    {
        if(GameCore.Instance.Master.CurrentRank >= GameCore.Instance.SelectedEnemy.Rank) GameCore.Instance.StartFight();
    }
    private void UpdateLabels()
    {
        SilverLabel?.SetText(GameCore.Instance.Master.Silver.ToString());
        var rankName = GameCore.Instance.Ranks[GameCore.Instance.Master.CurrentRank].Name;
        RankLabel?.SetText("Ранг: " + rankName.ToLower());
    }
}
