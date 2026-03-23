using UnityEngine;
using TMPro;
[System.Serializable]
public class MeditationController : MonoBehaviour
{
    [SerializeField] private QiOrbController QiOrb;
    [SerializeField] private BreathingController Breathing;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI TimeLeftLabel;
    [SerializeField] private TextMeshProUGUI NeedSpeedLabel;
    [SerializeField] private TextMeshProUGUI OrbSpeedLabel;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject BackButton;
    [HideInInspector] public bool IsRunning = false;

    [SerializeField] private float Duration = 20f;
    [SerializeField] private int QiBonus = 1;
    
    private CharacterData master;

    public static MeditationController Instance;
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
        IsRunning = true;
        ToggleElements(true);
        BackButton.SetActive(false);

        Breathing.StartBreathing();
        QiOrb.StartMoving();
    }
    private void EndSession()
    {        
        IsRunning = false;
        ToggleElements(false);
        BackButton.SetActive(true);

        Breathing.StopBreathing();
        QiOrb.StopMoving();
        GameCore.Instance.AdvanceTime(1);
    }
    private void ToggleElements(bool value)
    {
        QiOrb.gameObject.SetActive(value);
        TimeLeftLabel.gameObject.SetActive(value);
        OrbSpeedLabel.gameObject.SetActive(value);
        NeedSpeedLabel.gameObject.SetActive(value);
    }
    private int SecondsLeft()
    {
        return (int)Mathf.Round(Duration - Breathing.SessionTime);
    }
    private void FixedUpdate() 
    {
        TimeLeftLabel.SetText("Осталось времени: " + SecondsLeft().ToString());
        StartButton.SetActive(!IsRunning && master.Qi < master.MaxQi);
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
    } 
    void Update()
    {
        if (!IsRunning) return;
        OrbSpeedLabel.SetText("Текущая скорость: " + QiOrb.CurrentSpeed.ToString("F1"));
        NeedSpeedLabel.SetText("Требуется: " + (QiOrb.StartSpeed + Breathing.CurrentPhase).ToString("F1"));

        if (Breathing.InRhythm(QiOrb.GetSpeedDelta()) && QiOrb.PassBottom()) master.AddQi(QiBonus);
        
        if (Breathing.SessionTime >= Duration) EndSession();
    }
}