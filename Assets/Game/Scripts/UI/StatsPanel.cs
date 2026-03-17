using UnityEngine;
using TMPro;

public class StatsPanel : MonoBehaviour
{
    private CharacterData character;
    [SerializeField] private TextMeshProUGUI BodyLabel;
    [SerializeField] private TextMeshProUGUI SpiritLabel;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI SilverLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;
    [SerializeField] private TextMeshProUGUI AgeLabel;
    void Awake()
    {
        var gameCore = FindFirstObjectByType<GameCore>();
        character = gameCore?.Run?.CurrentMaster;
    }
    private void Start()
    {
        UpdateLabels();
    }
    public void UpdateLabels()
    {
        BodyLabel?.SetText("Тело: " + character.Body);
        SpiritLabel?.SetText("Дух: " + character.Spirit);
        QiLabel?.SetText($"Ци: {character.Qi} / {character.MaxQi}");
        SilverLabel?.SetText("Серебро: " + character.Silver);
        RankLabel?.SetText("Ранг: " + character.Rank);
        AgeLabel?.SetText("Возраст: " + character.Age);
    }
}