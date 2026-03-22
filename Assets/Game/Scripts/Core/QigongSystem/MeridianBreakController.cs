using UnityEngine;
using TMPro;
public class MeridianBreakController : MonoBehaviour 
{
    [SerializeField] private QiOrbController QiOrb;
    [SerializeField] private BreathingController Breathing;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI ShootLabel;
    [SerializeField] private TextMeshProUGUI NeedSpeedLabel;
    [SerializeField] private TextMeshProUGUI OrbSpeedLabel;
    [SerializeField] private float NormalRhytmRatio = 0.4f;
    [SerializeField] private float GoodRhytmRatio = 0.7f;
    [SerializeField] private int LostQi = 2;
    private CharacterData master;

    public static MeridianBreakController Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        master = GameCore.Instance.CurrentMaster;
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        StartSession();
    }
    public void StartSession()
    {
        Breathing.StartBreathing();
        QiOrb.StartMoving();
    }
    private void FixedUpdate()
    {
        QiLabel?.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        if (master.Qi > 0) ShootLabel.SetText("Нажмите F для броска");
        else ShootLabel.SetText("Недостаточно ци для броска");
    }
    void Update()
    {
        OrbSpeedLabel.SetText("Текущая скорость: " + QiOrb.CurrentSpeed.ToString("F1"));
        NeedSpeedLabel.SetText("Требуется: " + (QiOrb.StartSpeed + Breathing.CurrentPhase).ToString("F1"));

        if (!QiOrb.OnDantian && !Breathing.InRhythm(QiOrb.GetSpeedDelta()) && Breathing.GetSeconds() < Breathing.GetSecondsRounded()) 
        {
            QiOrb.SpendQi(LostQi);
        } 
    }
}
