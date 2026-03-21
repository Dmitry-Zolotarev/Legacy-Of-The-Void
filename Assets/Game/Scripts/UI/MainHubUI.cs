using UnityEngine;
using TMPro;
public class MainHubUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SilverAmountLabel;
    [SerializeField] private TextMeshProUGUI TrophiesLabel; 
    [SerializeField] private TextMeshProUGUI GenerationLabel;
    [SerializeField] private TextMeshProUGUI AgeLabel;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;
    [SerializeField] private TextMeshProUGUI HasStudentLabel;

    public static MainHubUI Instance;
    private CharacterData master;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start() {
        master = GameCore.Instance.CurrentMaster;
        UpdateLabels();
    }
    
    private void FixedUpdate() => UpdateLabels();
    private void UpdateLabels()
    {
        SilverAmountLabel?.SetText(master.Silver.ToString());
        TrophiesLabel?.SetText(master.Trophies.ToString()); 
        GenerationLabel?.SetText("Поколение: " + master.Generation);
        AgeLabel?.SetText("Возраст: " + master.Age);
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        RankLabel?.SetText("Ранг: " + master.Ranks[master.CurrentRank].Name);
        HasStudentLabel?.SetText("Ученик: " + master.GetStudentName());
    }
}