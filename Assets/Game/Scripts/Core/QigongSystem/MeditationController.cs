using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public class MeditationController : MonoBehaviour
{
    [SerializeField] private QiOrbController QiOrb;
    [SerializeField] private BreathingController Breathing;  
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject BackButton;     
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private Image QiFluid;
    [SerializeField] private TextMeshProUGUI TimeLeftLabel;
    [SerializeField] private TextMeshProUGUI QiElixirsLabel;

    [SerializeField] private GameObject QiElixirsPanel;
    [SerializeField] private GameObject MouseButtonsHint;
    [SerializeField] private GameObject AgeLabel;
    [SerializeField] private int StartQiBonus = 2;
    [SerializeField] private int ElixirPower = 2;
    [SerializeField] private float GiveQiTick = 0.05f;
    [SerializeField] private float Duration = 30f;
    [HideInInspector] public bool IsRunning = false;
    public static MeditationController Instance;
    private CharacterData master;
    private float QiBonus = 2;
    float lastGiveTime;
    private float QiGained = 0f;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void OnEnable()
    {
        master = GameCore.Instance.Master;
        
        QiElixirsLabel.SetText(master.QiElixirs.ToString());
        UpdateUI();
        ToggleElements(false);
    }
    public void StartSession()
    {
        QiGained = 0f;
        QiBonus = StartQiBonus;
        if (master.QiElixirs > 0) 
        {
            QiBonus = StartQiBonus * ElixirPower;
            master.QiElixirs--;
        }
        ToggleElements(true);
        MouseButtonsHint.SetActive(false);
        Breathing.StartBreathing();
        QiOrb.StartMoving();
        lastGiveTime = Time.time;
    }
    private void EndSession()
    {      
        QiElixirsLabel.SetText(master.QiElixirs.ToString());
        master.AddQi((int)QiGained);
        GameCore.Instance.AdvanceTime(1);
        ToggleElements(false);
        QiOrb.StopMoving();
        QiGained = 0f;
    }
    private void ToggleElements(bool value)
    {     
        IsRunning = value;
        QiOrb?.gameObject.SetActive(value);     
        TimeLeftLabel?.gameObject.SetActive(value);

        QiElixirsPanel?.SetActive(!value);
        AgeLabel?.SetActive(!value);    
        BackButton?.SetActive(!value);
        StartButton?.gameObject.SetActive(!value && master.Qi < master.MaxQi);

        if(!IsRunning)
        {
            QiLabel.SetText($"Ци: {master.Qi} / {master.MaxQi}"); ;
            QiLabel.color = Color.white;
        }      
    }
    private int SecondsLeft()
    {
        return (int)Mathf.Round(Duration - Breathing.SessionTime);
    } 
    private void UpdateUI()
    {
        TimeLeftLabel.SetText(SecondsLeft().ToString());
        StartButton.SetActive(!IsRunning && master.Qi < master.MaxQi);
        float currentQi = master.Qi + QiGained;
        QiFluid.fillAmount = currentQi / master.MaxQi;
    }
    private void SetRhythmLabelText()
    {
        if (Breathing.SlowOrFast(QiOrb.GetSpeedDelta()) != 0)
        {
            QiLabel.color = Color.orangeRed;
        }
        else QiLabel.color = Color.green;

        if (Breathing.SlowOrFast(QiOrb.GetSpeedDelta()) < 0)
        {
            QiLabel.SetText("Быстрее ↑");
        }
        else if (Breathing.SlowOrFast(QiOrb.GetSpeedDelta()) > 0)
        {
            QiLabel.SetText("Медленнее ↓");
        }
        else QiLabel.SetText("В ритме");
    }
    void Update()
    { 
        if (!IsRunning) return;
        SetRhythmLabelText();
        if (Breathing.InRhythm(QiOrb.GetSpeedDelta()))
        {
            QiGained += QiBonus * Time.deltaTime; 
        }
        UpdateUI();
        if (Breathing.SessionTime >= Duration || (int)(master.Qi + QiGained) >= master.MaxQi) EndSession();
    }
}