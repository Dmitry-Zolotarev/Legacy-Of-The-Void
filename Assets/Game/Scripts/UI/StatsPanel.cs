using UnityEngine;
using TMPro;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI BodyLabel;
    [SerializeField] private TextMeshProUGUI SpiritLabel;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI GenLabel;
    [SerializeField] private TextMeshProUGUI SilverLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;
    [SerializeField] private TextMeshProUGUI AgeLabel;
    [SerializeField] private TextMeshProUGUI StatusLabel;

    public static StatsPanel Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start()
    {
        UpdateLabels();
    }
    public void UpdateLabels()
    {
        var master = GameCore.Instance.Run.CurrentMaster;

        BodyLabel?.SetText("Тело: " + master.Body);
        SpiritLabel?.SetText("Дух: " + master.Spirit);
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        GenLabel?.SetText("Поколение: " + master.Generation);
        SilverLabel?.SetText("Серебро: " + master.Silver);
        RankLabel?.SetText("Ранг: " + master.Rank);
        AgeLabel?.SetText("Возраст: " + master.Age);
        StatusLabel?.SetText("Статус: " + master.currentState.ToString());
    }
}