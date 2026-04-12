using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RankSystemUI : MonoBehaviour
{
    [HideInInspector] public bool ShowStudentWindow = false;
    [SerializeField] private TextMeshProUGUI CurrentRankLabel;
    [SerializeField] private TextMeshProUGUI NextRankLabel;
    [SerializeField] private TextMeshProUGUI NeedBodyLabel;
    [SerializeField] private TextMeshProUGUI NeedMeridiansLabel;
    [SerializeField] private TextMeshProUGUI NeedQiLabel;
    [SerializeField] private GameObject FinalVoidBreakCanvas;
    [SerializeField] private GameObject StudentUnlockedWindow;
    [SerializeField] private TextMeshProUGUI StudentNameLabel;
    [SerializeField] private Slider rankBar;
    
    public static RankSystemUI Instance;
    private CharacterData master;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void OnEnable()
    {
        UpdateLabels();

        if (master.CurrentRank >= GameCore.Instance.Ranks.Count - 1) FinalVoidBreakCanvas?.SetActive(true);

        StudentUnlockedWindow.SetActive(ShowStudentWindow);

        if(ShowStudentWindow) StudentNameLabel.SetText(master.Student.GetFullName());

        ShowStudentWindow = false;
    }
    public void UpdateLabels()
    {
        master = GameCore.Instance.Master;
        rankBar.value = master.CurrentRank;
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
