using UnityEngine;
using TMPro;

public class RankSystemUI : MonoBehaviour
{
    
    public static RankSystemUI Instance;
    [SerializeField] private TextMeshProUGUI currentRankLabel;
    [SerializeField] private TextMeshProUGUI nextRankLabel;
    [SerializeField] private TextMeshProUGUI needBodyLabel;
    [SerializeField] private TextMeshProUGUI needMeridiansLabel;
    [SerializeField] private TextMeshProUGUI needQiLabel;
    private CharacterData master;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void UpdateLabels()
    {
        currentRankLabel.SetText(master.Ranks[master.CurrentRank].Name);
        nextRankLabel.SetText(master.GetNextRank().Name);
        needBodyLabel.SetText($"Тело: {master.Body} / {master.GetNextRank().needBody}");
        needMeridiansLabel.SetText($"Меридианы: {master.OpenedMeridians} / {master.GetNextRank().needMeridians}");
        needQiLabel.SetText($"Текущая ци: {master.Qi} / {master.MaxQi / 2}");
    }
    public void Start()
    {
        master = GameCore.Instance.CurrentMaster;
        UpdateLabels();
    }
    public void TryRankBreakthrough()
    {
        if (master.Body >= master.GetNextRank().needBody && master.OpenedMeridians >= master.GetNextRank().needMeridians && master.Qi >= master.MaxQi / 2)
        {
            if (master.CurrentRank < master.Ranks.Count - 1) ScreenManager.Instance.OpenMenu(7);
        }          
    }
    public void FixedUpdate() => UpdateLabels(); 
}
