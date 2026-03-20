using UnityEngine;
using TMPro;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI BodyLabel;
    [SerializeField] private TextMeshProUGUI SpiritLabel;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI GenLabel;
    [SerializeField] private TextMeshProUGUI MeridianLabel;
    [SerializeField] private TextMeshProUGUI SilverLabel;
    [SerializeField] private TextMeshProUGUI TrophiesLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;
    [SerializeField] private TextMeshProUGUI AgeLabel;
    [SerializeField] private TextMeshProUGUI StatusLabel;

    public static StatsPanel Instance;
    private CharacterData master;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start() {
        master = GameCore.Instance.Run.CurrentMaster;
        UpdateLabels();
    }

    private void FixedUpdate() => UpdateLabels();
    private void UpdateLabels()
    {
        BodyLabel?.SetText("Тело: " + master.Body);
        SpiritLabel?.SetText("Дух: " + master.Spirit);
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        GenLabel?.SetText("Поколение: " + master.Generation);
        MeridianLabel?.SetText("Поколение: " + master.CurrentMeridian);
        SilverLabel?.SetText("Серебро: " + master.Silver);
        TrophiesLabel?.SetText("Трофеи: " + master.Trophies);
        RankLabel?.SetText("Ранг: " + master.Rank);
        AgeLabel?.SetText("Возраст: " + master.Age);
        StatusLabel?.SetText("Статус: " + master.currentState.ToString());
    }
}