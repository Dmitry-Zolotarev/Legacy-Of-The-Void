using UnityEngine;

[System.Serializable]
public class MeditationController : MonoBehaviour
{
    public MeditationMode Mode = MeditationMode.Normal;
    public MeditationState State = MeditationState.Idle;
    public MeditationQuality Quality = MeditationQuality.Excellent;

    public float StartRhythmWindow = 1f;
    [HideInInspector] public float RhythmWindow = 1f;
    public float RhythmAmplitude = 1.5f;

    [SerializeField] private float Duration = 20f;
    [HideInInspector] public float RhythmSpeed = 1f;
    [HideInInspector] public float FlowSpeed = 1f;

    private CharacterData master;
    private float TimeInRhythm = 0f;   
    private float Timer = 0f;
    public float AccelerationDelta = 1f;
    public float MaxDeviation = 5f;
    public bool InRhythm = true;
    public static MeditationController Instance;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start() => master = GameCore.Instance.Run.CurrentMaster;
    public void ToggleSession()
    {
        if (State != MeditationState.Running) StartSession();
        else EndSession();
    }
    
    private void StartSession()
    {
        if (master.Qi >= master.MaxQi) return;

        RhythmWindow = StartRhythmWindow;
        if (Mode == MeditationMode.RiskyBreakthrough) RhythmWindow /= 2f;

        State = MeditationState.Running;
        MeditationUI.Instance.ToggleElements();
        MeditationUI.Instance.ResultPanel.SetActive(false);
        Quality = MeditationQuality.Excellent;      
        
        FlowSpeed = 0f;
        TimeInRhythm = 0f;       
        Timer = 0f;
    }
    public bool IsBreakthrough()
    {
        return Mode == MeditationMode.StableBreakthrough || Mode == MeditationMode.RiskyBreakthrough;
    }
    public void EndSession()
    {
        State = MeditationState.Idle;
        MeditationUI.Instance.ToggleElements();     

        float ratio = GetSuccessRatio();
        if (Timer < Duration) Quality = MeditationQuality.Disrupted;
        else if (ratio <= 0.4f) Quality = MeditationQuality.Bad;
        else if (ratio <= 0.7f) Quality = MeditationQuality.Normal;

        MeditationUI.Instance.ShowResult();
        if (Mode == MeditationMode.Normal)
        {
            master.Qi += GetQiReward();

            if (master.Qi >= master.MaxQi) master.Qi = master.MaxQi;
        }
        else if (IsBreakthrough() && InRhythm && Quality == MeditationQuality.Excellent) master.OpenMeridian();
        GameCore.Instance.AdvanceTime(1);
    }
    public float GetSuccessRatio()
    {
        if (Timer <= 0) return 0;
        return TimeInRhythm / Timer;
    }
    public int GetQiReward()
    {
        switch (Quality)
        {
            case MeditationQuality.Normal: return 3;
            case MeditationQuality.Excellent: return 5;
        }
        return 0;
    }
    public int SecondsLeft()
    {
        return (int)Mathf.Round(Duration - Timer);
    }

    public float GetAccelerationDelta()
    {
        float delta = 0f;
        if (Input.GetMouseButton(1)) delta += AccelerationDelta;
        if (Input.GetMouseButton(0)) delta -= AccelerationDelta;
        return delta;
    }
    
    void Update()
    {
        if (State != MeditationState.Running) return;

        var rhythmDelta = RhythmSpeed;
        RhythmSpeed = Mathf.Sin(Timer) * RhythmAmplitude;
        rhythmDelta = RhythmSpeed - rhythmDelta;

        FlowSpeed += GetAccelerationDelta() * Time.deltaTime - rhythmDelta;
        FlowSpeed = Mathf.Clamp(FlowSpeed, -MaxDeviation, MaxDeviation);
        
        InRhythm = FlowSpeed > RhythmSpeed - RhythmWindow && FlowSpeed < RhythmSpeed + RhythmWindow;

        Timer += Time.deltaTime;
        if (InRhythm) TimeInRhythm += Time.deltaTime;
        if (Timer >= Duration) EndSession();
        MeditationUI.Instance.UpdateLabels();
    }
}