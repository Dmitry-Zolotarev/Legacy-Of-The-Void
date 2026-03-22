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

    [SerializeField] private float NormalRhytmRatio = 0.4f;
    [SerializeField] private float GoodRhytmRatio = 0.7f;

    [SerializeField] private float Duration = 20f;
    [SerializeField] private int QiBonus = 10;
    private CharacterData master;
    private bool IsRunning = false;
    private float TimeInRhythm = 0f;
    

    public static MeditationController Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start() 
    {
        master = GameCore.Instance.CurrentMaster;
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
    }
    public void StartSession()
    {
        IsRunning = true;
        QiOrb.gameObject.SetActive(true);
        TimeLeftLabel.gameObject.SetActive(true);
        OrbSpeedLabel.gameObject.SetActive(true);
        NeedSpeedLabel.gameObject.SetActive(true);
        Cursor.visible = false;
        BackButton.SetActive(false);
        Breathing.StartBreathing();
        QiOrb.StartMoving();
    }
    private void EndSession()
    {        
        IsRunning = false;
        QiOrb.gameObject.SetActive(false);
        TimeLeftLabel.gameObject.SetActive(false);
        OrbSpeedLabel.gameObject.SetActive(false);
        NeedSpeedLabel.gameObject.SetActive(false);
        Cursor.visible = true;
        BackButton.SetActive(true);
        Breathing.StopBreathing();
        QiOrb.StopMoving();
        GetQiReward();
        GameCore.Instance.AdvanceTime(1);
    }
    private void GetQiReward()
    {        
        var rhythmRatio = TimeInRhythm / Breathing.SessionTime;
        if (rhythmRatio >= GoodRhytmRatio) master.AddQi(QiBonus);
        else if (rhythmRatio >= NormalRhytmRatio) master.AddQi(QiBonus / 2);
        
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
        if (Breathing.InRhythm(QiOrb.GetSpeedDelta())) TimeInRhythm += Time.deltaTime;
        if (Breathing.SessionTime >= Duration) EndSession();
    }
}