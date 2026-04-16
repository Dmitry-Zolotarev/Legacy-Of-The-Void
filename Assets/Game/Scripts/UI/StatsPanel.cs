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
    [SerializeField] private TextMeshProUGUI InternalDemonLabel;

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
        try
        {
            master = GameCore.Instance.Master;
            GenerationLabel?.SetText("Поколение: " + master.Generation);
            MasterNameLabel?.SetText($"Мастер {master.Name}, {master.Age} {GameCore.Instance.GetYearWord(master.Age)}");
            AgeLabel?.SetText($"Возраст: {master.Age} {GameCore.Instance.GetYearWord(master.Age)}");

            BodyLabel?.SetText($"Телосложение: {master.Body} / {master.MaxBody}");
            if (BodyBar != null) BodyBar.value = (float)master.Body / master.MaxBody;

            QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
            if (QiBar != null) QiBar.value = (float)master.Qi / master.MaxQi;

            InternalDemonLabel?.SetText($"Внутренний демон: {master.InternalDemon.GetCurrentState().Name.ToLower()}");

            MeridiansLabel?.SetText($"Открыто меридианов: {master.OpenedMeridians} / {GameCore.Instance.MeridianLevels.Count}");
            RankLabel?.SetText("Ранг: " + GameCore.Instance.Ranks[master.CurrentRank].Name.ToLower());

            HasStudentLabel?.SetText(master.Student == null ? "Нет ученика" : $"Ученик: {master.Student.GetFullName()}");
        }
        catch { }       
    }
}