using UnityEngine;
using TMPro;
public class StatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MasterNameLabel;
    [SerializeField] private TextMeshProUGUI GenerationLabel;
    [SerializeField] private TextMeshProUGUI AgeLabel;
    [SerializeField] private TextMeshProUGUI BodyLabel;
    [SerializeField] private TextMeshProUGUI SpiritLabel;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI MeridiansLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;   
    [SerializeField] private TextMeshProUGUI HealthStateLabel;
    [SerializeField] private TextMeshProUGUI HasStudentLabel;
    private CharacterData master;
 
    public void Start()
    {
        master = GameCore.Instance.CurrentMaster;
        UpdateLabels();
    }
    private void FixedUpdate() => UpdateLabels();
    private void UpdateLabels()
    {
        MasterNameLabel?.SetText("Мастер " + master.Name);
        GenerationLabel?.SetText("Поколение: " + master.Generation);
        AgeLabel?.SetText("Возраст: " + master.Age);
        BodyLabel?.SetText("Тело: " + master.Body);
        SpiritLabel?.SetText("Дух: " + master.Spirit);
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        MeridiansLabel?.SetText($"Открыто меридианов: {master.OpenedMeridians} / {master.MeridianLevels.Count}");
        RankLabel?.SetText("Ранг: " + master.Ranks[master.CurrentRank].Name);
        HealthStateLabel?.SetText(GameCore.GetEnumDescription(master.currentState));
        HasStudentLabel?.SetText("Ученик: " + master.GetStudentName());
    }
}