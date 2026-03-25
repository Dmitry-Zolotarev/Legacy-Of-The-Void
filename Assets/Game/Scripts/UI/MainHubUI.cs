using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainHubUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SilverAmountLabel;
    [SerializeField] private TextMeshProUGUI GenerationLabel;
    [SerializeField] private TextMeshProUGUI AgeLabel;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;
    [SerializeField] private TextMeshProUGUI HasStudentLabel;
    [SerializeField] private Image MasterSprite;
    [SerializeField] private Sprite YoungMasterSprite, AdultMasterSprite, OldMasterSprite;
    public static MainHubUI Instance;
    private CharacterData master;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start() => FixedUpdate();
    private void FixedUpdate() 
    {
        master = GameCore.Instance.Master;
        UpdateLabels();
        UpdateMasterSprite();
    } 
    private void UpdateMasterSprite()
    {
        if (master.Age >= 60)
        {
            MasterSprite.sprite = OldMasterSprite;
        }
        else if (master.Age >= 40)
        {
            MasterSprite.sprite = AdultMasterSprite;
        }
        else MasterSprite.sprite = YoungMasterSprite;
    }
    private void UpdateLabels()
    {
        SilverAmountLabel?.SetText(master.Silver.ToString());
        GenerationLabel?.SetText("Поколение: " + master.Generation);
        AgeLabel?.SetText("Возраст: " + master.Age);
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        RankLabel?.SetText("Ранг: " + master.Ranks[master.CurrentRank].Name.ToLower());
        HasStudentLabel?.SetText("Ученик: " + master.GetStudentName());
    }
}