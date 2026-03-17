using UnityEngine;
using TMPro;

public class StatsPanel : MonoBehaviour
{
    private CharacterData Master;
    [SerializeField] private TextMeshProUGUI BodyLabel;
    [SerializeField] private TextMeshProUGUI SpiritLabel;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI GenLabel;
    [SerializeField] private TextMeshProUGUI SilverLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;
    [SerializeField] private TextMeshProUGUI AgeLabel;
    [SerializeField] private TextMeshProUGUI StatusLabel;
    void Awake()
    {
        var gameCore = FindFirstObjectByType<GameCore>();
        Master = gameCore?.Run?.CurrentMaster;
    }
    private void Start()
    {
        UpdateLabels();
    }
    public void UpdateLabels()
    {
        BodyLabel?.SetText("Тело: " + Master.Body);
        SpiritLabel?.SetText("Дух: " + Master.Spirit);
        QiLabel?.SetText($"Ци: {Master.Qi} / {Master.MaxQi}");
        GenLabel?.SetText("Поколение: " + Master.Generation);
        SilverLabel?.SetText("Серебро: " + Master.Silver);
        RankLabel?.SetText("Ранг: " + Master.Rank);
        AgeLabel?.SetText("Возраст: " + Master.Age);
        StatusLabel?.SetText("Статус: " + Master.currentState.ToString());
    }
}