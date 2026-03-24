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

    [SerializeField] private float Duration = 20f;
    [SerializeField] private int QiBonus = 1;

    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI TimeLeftLabel;
    [SerializeField] private TextMeshProUGUI InRhythmLabel;
    [SerializeField] private GameObject MouseButtonsHint;

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
        ToggleElements(true);
        

        Breathing.StartBreathing();
        QiOrb.StartMoving();
    }
    private void EndSession()
    {         
        ToggleElements(false);
        Breathing.StopBreathing();
        QiOrb.StopMoving();
        GameCore.Instance.AdvanceTime(1);
    }
    private void ToggleElements(bool value)
    {
        IsRunning = value;
        QiOrb.gameObject.SetActive(value);
        TimeLeftLabel.gameObject.SetActive(value);
        InRhythmLabel.gameObject.SetActive(value);
        MouseButtonsHint.SetActive(value);
        
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
        TimeLeftLabel.SetText("Осталось времени: " + SecondsLeft().ToString());
        StartButton.SetActive(!IsRunning && master.Qi < master.MaxQi);
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
    } 
    private void SetRhythmLabelText()
    {
        if (Breathing.SlowOrFast(QiOrb.GetSpeedDelta()) != 0) InRhythmLabel.color = Color.orangeRed;
        else InRhythmLabel.color = Color.green;

        if (Breathing.SlowOrFast(QiOrb.GetSpeedDelta()) < 0)
        {
            InRhythmLabel.SetText("Слишком медленно");
        }
        else if (Breathing.SlowOrFast(QiOrb.GetSpeedDelta()) > 0) 
        {
            InRhythmLabel.SetText("Слишком быстро");
        } 
        else InRhythmLabel.SetText("В ритме");
    }
    void Update()
    {
        if (!IsRunning) return;
        SetRhythmLabelText();
        if (Breathing.InRhythm(QiOrb.GetSpeedDelta()) && QiOrb.PassBottom()) master.AddQi(QiBonus);     
        if (Breathing.SessionTime >= Duration || master.Qi >= master.MaxQi) EndSession();
    }
}