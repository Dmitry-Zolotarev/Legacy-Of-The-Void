using UnityEngine;
using TMPro;

public class RankSystemUI : MonoBehaviour
{
    public static RankSystemUI Instance;
    [SerializeField] private TextMeshProUGUI CurrentRankLabel;
    [SerializeField] private TextMeshProUGUI NextRankLabel;
    [SerializeField] private TextMeshProUGUI NeedBodyLabel;
    [SerializeField] private TextMeshProUGUI NeedMeridiansLabel;
    [SerializeField] private TextMeshProUGUI NeedQiLabel;
    [SerializeField] private GameObject FinalVoidBreakCanvas;
    [SerializeField] private GameObject StudentUnlockedWindow;
    [SerializeField] private TextMeshProUGUI StudentNameLabel;
    private bool StudentMessageShown = false;
    private CharacterData master;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void OnEnable()
    {
        UpdateLabels();
        if (master.CurrentRank >= GameCore.Instance.Ranks.Count - 1) FinalVoidBreakCanvas?.SetActive(true);
        if(master.CurrentRank == (int)master.RankForBecomeTeacher && !StudentMessageShown)
        {
            StudentUnlockedWindow.SetActive(true);
            StudentNameLabel.SetText(master.Student.GetFullName());
            StudentMessageShown = true;
        }
    }
    private void OnDisable()
    {
        StudentUnlockedWindow.SetActive(false);
    }
    public void UpdateLabels()
    {
        if(master != GameCore.Instance.Master) StudentMessageShown = false;

        master = GameCore.Instance.Master;
        CurrentRankLabel.SetText(GameCore.Instance.Ranks[master.CurrentRank].Name);
        NextRankLabel.SetText(master.GetNextRank().Name);
        NeedBodyLabel.SetText($"Телосложение: {master.Body} / {GameCore.Instance.Ranks[master.CurrentRank].MaxBody}");
        NeedMeridiansLabel.SetText($"Меридианы: {master.OpenedMeridians} / {master.GetNextRank().needMeridians}");
        NeedQiLabel.SetText($"Текущая ци: {master.Qi} / {master.GetNextRankID() * 20}");
    }   
    public void TryRankBreakthrough()
    {
        var currentRank = GameCore.Instance.Ranks[master.CurrentRank];
        
        if (master.Body >= currentRank.MaxBody && master.OpenedMeridians >= master.GetNextRank().needMeridians && master.Qi >= master.GetNextRankID() * 20)
        {
            if (master.CurrentRank < GameCore.Instance.Ranks.Count - 1) ScreenManager.Instance.OpenMenu(7);
        }          
    }
}
