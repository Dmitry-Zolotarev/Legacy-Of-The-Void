using UnityEngine;
using TMPro;
public class StatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MasterNameLabel;
    [SerializeField] private TextMeshProUGUI GenerationLabel;
    [SerializeField] private TextMeshProUGUI AgeLabel;
    [SerializeField] private TextMeshProUGUI BodyLabel;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI MeridiansLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;   
    [SerializeField] private TextMeshProUGUI HasStudentLabel;
    [SerializeField] private bool DoRegularUpdate = false;
    private CharacterData master;
 
    public void OnEnable()
    {
        UpdateLabels();
    }
    private void FixedUpdate() 
    {
        if(DoRegularUpdate) UpdateLabels();
    }
    
    private void UpdateLabels()
    {
        master = GameCore.Instance.Master;

        MasterNameLabel?.SetText("Мастер " + master.Name);
        GenerationLabel?.SetText("Поколение: " + master.Generation);
        AgeLabel?.SetText($"Возраст: " + master.Age);
        BodyLabel?.SetText("Телосложение: " + master.Body);
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        MeridiansLabel?.SetText($"Открыто меридианов: {master.OpenedMeridians} / {master.MeridianLevels.Count}");
        RankLabel?.SetText("Ранг: " + master.Ranks[master.CurrentRank].Name.ToLower());
        HasStudentLabel?.SetText("Ученик: " + master.GetStudentName());
    }
}