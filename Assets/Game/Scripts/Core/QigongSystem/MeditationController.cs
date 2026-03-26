using UnityEngine;
using TMPro;
[System.Serializable]
public class MeditationController : MonoBehaviour
{
    [SerializeField] private QiOrbController QiOrb;
    [SerializeField] private BreathingController Breathing;  
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject BackButton;     
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI TimeLeftLabel;
    [SerializeField] private TextMeshProUGUI InRhythmLabel;
    [SerializeField] private TextMeshProUGUI QiElixirsLabel;

    [SerializeField] private GameObject QiElixirsPanel;
    [SerializeField] private GameObject MouseButtonsHint;
    [SerializeField] private GameObject AgeLabel;
    [SerializeField] private int StartQiBonus = 1;
    [SerializeField] private int ElixirPower = 2;
    [SerializeField] private float GiveQiTick = 0.5f;
    [SerializeField] private float Duration = 30f;
    [HideInInspector] public bool IsRunning = false;
    public static MeditationController Instance;
    private CharacterData master;
    private int QiBonus = 1;
    float lastGiveTime;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void OnEnable()
    {
        master = GameCore.Instance.Master;
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        QiElixirsLabel.SetText(master.QiElixirs.ToString());
        ToggleElements(false);
    }
    public void StartSession()
    {
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
        ToggleElements(false);
        QiElixirsLabel.SetText(master.QiElixirs.ToString()); 
        GameCore.Instance.AdvanceTime(1);
        QiLabel.color = Color.white;
        QiOrb.StopMoving();
    }
    private void ToggleElements(bool value)
    {
        IsRunning = value;
        QiOrb?.gameObject.SetActive(value);
        TimeLeftLabel?.gameObject.SetActive(value);
        InRhythmLabel?.gameObject.SetActive(value);

        QiElixirsPanel?.SetActive(!value);
        AgeLabel?.SetActive(!value);    
        BackButton?.SetActive(!value);
    }
    private int SecondsLeft()
    {
        return (int)Mathf.Round(Duration - Breathing.SessionTime);
    }
    private void FixedUpdate() 
    {
        master = GameCore.Instance.Master;
        TimeLeftLabel.SetText(SecondsLeft().ToString());
        StartButton.SetActive(!IsRunning && master.Qi < master.MaxQi);
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
    } 
    private void SetRhythmLabelText()
    {
        if (Breathing.SlowOrFast(QiOrb.GetSpeedDelta()) != 0)
        {
            InRhythmLabel.color = Color.orangeRed;
            QiLabel.color = Color.white;
        }
        else 
        {
            InRhythmLabel.color = Color.green;
            QiLabel.color = Color.green;
        } 
        if (Breathing.SlowOrFast(QiOrb.GetSpeedDelta()) < 0)
        {
            InRhythmLabel.SetText("Быстрее ↑");
        }
        else if (Breathing.SlowOrFast(QiOrb.GetSpeedDelta()) > 0) 
        {
            InRhythmLabel.SetText("Медленнее ↓");
        } 
        else InRhythmLabel.SetText("В ритме");
    }
    void Update()
    {
        
        if (!IsRunning) return;
        SetRhythmLabelText();
        if (Breathing.InRhythm(QiOrb.GetSpeedDelta()) && Time.time > lastGiveTime + GiveQiTick)
        {
            master.AddQi(QiBonus);
            lastGiveTime = Time.time;
        }    
        if (Breathing.SessionTime >= Duration || master.Qi >= master.MaxQi) EndSession();
    }
}