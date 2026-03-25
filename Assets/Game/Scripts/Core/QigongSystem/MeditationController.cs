using UnityEngine;
using TMPro;
[System.Serializable]
public class MeditationController : MonoBehaviour
{
    [SerializeField] private QiOrbController QiOrb;
    [SerializeField] private BreathingController Breathing;
    
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject BackButton;
    [HideInInspector] public bool IsRunning = false;

    [SerializeField] private float Duration = 30f;

    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI TimeLeftLabel;
    [SerializeField] private TextMeshProUGUI InRhythmLabel;
    [SerializeField] private GameObject MouseButtonsHint;
    [SerializeField] private GameObject AgeLabel;

    private CharacterData master;

    public static MeditationController Instance;
    float lastGiveTime;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start() 
    {
        master = GameCore.Instance.CurrentMaster;
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        ToggleElements(false);
    }
    public void StartSession()
    {
        ToggleElements(true);
        MouseButtonsHint.SetActive(false);
        Breathing.StartBreathing();
        QiOrb.StartMoving();
        lastGiveTime = Time.time;
    }
    private void EndSession()
    {         
        ToggleElements(false);
        QiOrb.StopMoving();
        GameCore.Instance.AdvanceTime(1);
    }
    private void ToggleElements(bool value)
    {
        IsRunning = value;
        QiOrb.gameObject.SetActive(value);
        TimeLeftLabel.gameObject.SetActive(value);
        InRhythmLabel.gameObject.SetActive(value);

        AgeLabel.SetActive(!value);
        MouseButtonsHint.SetActive(!value);
        BackButton.SetActive(!value);
    }
    private int SecondsLeft()
    {
        return (int)Mathf.Round(Duration - Breathing.SessionTime);
    }
    private void FixedUpdate() 
    {
        master = GameCore.Instance.CurrentMaster;
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
        if (Breathing.InRhythm(QiOrb.GetSpeedDelta()) && Time.time >= lastGiveTime + 0.5f)
        {
            master.AddQi(1);
            lastGiveTime = Time.time;
        }    
        if (Breathing.SessionTime >= Duration || master.Qi >= master.MaxQi) EndSession();
    }
}