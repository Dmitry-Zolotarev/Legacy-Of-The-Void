using UnityEngine;
using TMPro;
public class StatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI BodyLabel;
    [SerializeField] private TextMeshProUGUI SpiritLabel;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI GenerationLabel;
    [SerializeField] private TextMeshProUGUI MeridianLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;
    [SerializeField] private TextMeshProUGUI AgeLabel;
    [SerializeField] private TextMeshProUGUI StatusLabel;
    [SerializeField] private TextMeshProUGUI HasStudentLabel;

    public static StatsPanel Instance;
    private CharacterData master;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void Start()
    {
        master = GameCore.Instance.CurrentMaster;
        UpdateLabels();
    }
    private void UpdateLabels()
    {
        BodyLabel?.SetText("Тело: " + master.Body);
        SpiritLabel?.SetText("Дух: " + master.Spirit);
        QiLabel?.SetText($"ЦИ: {master.Qi} / {master.MaxQi}");
        MeridianLabel?.SetText("Открыто меридианов: " + master.OpenedMeridians);
        GenerationLabel?.SetText("Поколение: " + master.Generation);
        RankLabel?.SetText("Ранг: " + master.Ranks[master.CurrentRank].Name);
        AgeLabel?.SetText("Возраст: " + master.Age);
        StatusLabel?.SetText("Ранения: " + GameCore.GetEnumDescription(master.currentState));
    }
}