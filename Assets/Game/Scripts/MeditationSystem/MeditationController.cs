using UnityEngine;

[System.Serializable]
public class MeditationController : MonoBehaviour
{
    public MeditationMode Mode = MeditationMode.Normal;
    public MeditationState State = MeditationState.Idle;
    public MeditationQuality meditationQuality = MeditationQuality.Excellent;

    public float RhythmWindow = 1f;
    public float RhythmAmplitude = 1.5f;

    [SerializeField] private int MaxDisruptions = 10;
    [SerializeField] private float Duration = 20f;

    [HideInInspector] public float RhythmSpeed = 1f;
    [HideInInspector] public float FlowSpeed = 1f;

    private float TimeInRhythm = 0f;   
    private int Disruptions = 0;
    private float Timer = 0f;
    public float AccelerationDelta = 1f;
    public float MaxDeviation = 5f;
    public bool InRhythm = true;

    public static MeditationController Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void ToggleSession()
    {
        if (State != MeditationState.Running) StartSession();
        else EndSession();
    }

    private void StartSession()
    {
        State = MeditationState.Running;
        MeditationUI.Instance.ToggleElements();
        MeditationUI.Instance.ResultPanel.SetActive(false);
        meditationQuality = MeditationQuality.Excellent;      

        FlowSpeed = 0f;
        TimeInRhythm = 0f;       
        Disruptions = 0;
        Timer = 0f;
    }

    public void EndSession()
    {
        State = MeditationState.Idle;
        MeditationUI.Instance.ToggleElements();
        var master = GameCore.Instance.Run.CurrentMaster;

        if (Disruptions >= MaxDisruptions)
        {
            meditationQuality = MeditationQuality.Disrupted;
        }
        else
        {
            float ratio = GetSuccessRatio();
            if (ratio <= 0.4f) meditationQuality = MeditationQuality.Bad;
            else if (ratio <= 0.7f) meditationQuality = MeditationQuality.Normal;
            else meditationQuality = MeditationQuality.Excellent;
        }
        master.Qi += GetQiReward();
        master.meditationStability += GetStabilityReward();

        if (master.Qi > master.MaxQi) master.Qi = master.MaxQi;
        GameCore.Instance.AdvanceTime(1);
        StartCoroutine(MeditationUI.Instance.ShowMeditationResult());
        
    }
    public float GetSuccessRatio() 
    {
        return TimeInRhythm / Duration;
    }
    public int GetQiReward()
    {
        switch (meditationQuality)
        {
            case MeditationQuality.Bad: return 1;
            case MeditationQuality.Normal: return 3;
            case MeditationQuality.Excellent: return 5;
        }
        return 0;
    }

    public int GetStabilityReward()
    {
        switch (meditationQuality)
        {
            case MeditationQuality.Normal: return 1;
            case MeditationQuality.Excellent: return 2;
        }
        return -1;
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

    void FixedUpdate()
    {
        if (State != MeditationState.Running) return;

        Timer += Time.fixedDeltaTime;

        FlowSpeed += GetAccelerationDelta() * Time.fixedDeltaTime;
        FlowSpeed = Mathf.Clamp(FlowSpeed, -MaxDeviation, MaxDeviation);

        RhythmSpeed =  -Mathf.Sin(Timer) * RhythmAmplitude;

        bool inRhythmNow = FlowSpeed > RhythmSpeed - RhythmWindow && FlowSpeed < RhythmSpeed + RhythmWindow;
        if (InRhythm && !inRhythmNow) Disruptions++;
        InRhythm = inRhythmNow;

        if (InRhythm) TimeInRhythm += Time.fixedDeltaTime;
        if (Timer >= Duration) EndSession();
        MeditationUI.Instance.UpdateLabels();
    }
}