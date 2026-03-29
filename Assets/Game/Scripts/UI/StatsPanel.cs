using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MasterNameLabel;
    [SerializeField] private TextMeshProUGUI GenerationLabel;
    [SerializeField] private TextMeshProUGUI AgeLabel;

    [SerializeField] private TextMeshProUGUI BodyLabel;
    [SerializeField] private Slider BodyBar;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private Slider QiBar;

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
    
    public void UpdateLabels()
    {
        master = GameCore.Instance.Master;
        MasterNameLabel?.SetText("Мастер " + master.Name);
        GenerationLabel?.SetText("Поколение: " + master.Generation);
        AgeLabel?.SetText($"Возраст: " + master.Age);

        BodyLabel?.SetText($"Телосложение: {master.Body} / {master.MaxBody}");
        if (BodyBar != null) BodyBar.value = (float)master.Body / master.MaxBody;

        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        if(QiBar != null) QiBar.value = (float)master.Qi / master.MaxQi;

        MeridiansLabel?.SetText($"Открыто меридианов: {master.OpenedMeridians} / {GameCore.Instance.MeridianLevels.Count}");
        RankLabel?.SetText("Ранг: " + GameCore.Instance.Ranks[master.CurrentRank].Name.ToLower());
        HasStudentLabel?.SetText("Ученик: " + master.GetStudentName());
    }
}