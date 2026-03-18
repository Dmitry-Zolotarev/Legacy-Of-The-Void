using UnityEngine;

[System.Serializable]
public class MeditationController : MonoBehaviour
{
    public MeditationMode Mode = MeditationMode.Normal;
    public MeditationState State = MeditationState.Idle;
    public MeditationQuality meditationQuality = MeditationQuality.Excellent;

    public float RhythmWindow = 1f;
    public float RhythmAmplitude = 2f;

    [SerializeField] private float StartFlowSpeed = 3f;
    [SerializeField] private float StartRhythmSpeed = 3f;   
    [SerializeField] private int MaxDisruptions = 10;
    [SerializeField] private float Duration = 30f;

    [HideInInspector] public float RhythmSpeed = 1f;
    [HideInInspector] public float FlowSpeed = 1f;

    private float TimeInRhythm = 0f;
    private bool InRhythm = true;
    private int Disruptions = 0;
    private float Timer = 0f;

    public float AccelerationDelta = 5f;

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
        meditationQuality = MeditationQuality.Excellent;
        State = MeditationState.Running;


        RhythmSpeed = StartRhythmSpeed;
        FlowSpeed = StartFlowSpeed;

        TimeInRhythm = 0f;
        Disruptions = 0;
        Timer = 0f;
    }

    public void EndSession()
    {
        State = MeditationState.Idle;

        var master = GameCore.Instance.Run.CurrentMaster;

        if (Disruptions >= MaxDisruptions)
        {
            meditationQuality = MeditationQuality.Disrupted;
        }
        else
        {
            float ratio = TimeInRhythm / Duration;
            if (ratio <= 0.4f) meditationQuality = MeditationQuality.Bad;
            else if (ratio <= 0.7f) meditationQuality = MeditationQuality.Normal;
            else meditationQuality = MeditationQuality.Excellent;
        }
        master.Qi += GetQiReward(meditationQuality);
        master.meditationStability += GetStabilityReward(meditationQuality);
        if (master.Qi > master.MaxQi) master.Qi = master.MaxQi;
        GameCore.Instance.AdvanceTime(1);
        Debug.Log("Медитация завершена");
    }

    private int GetQiReward(MeditationQuality quality)
    {
        switch (quality)
        {
            case MeditationQuality.Bad: return 1;
            case MeditationQuality.Normal: return 3;
            case MeditationQuality.Excellent: return 5;
        }
        return 0;
    }

    private int GetStabilityReward(MeditationQuality quality)
    {
        switch (quality)
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
        if (Input.GetMouseButton(1)) delta += AccelerationDelta * Time.deltaTime;
        if (Input.GetMouseButton(0)) delta -= AccelerationDelta * Time.deltaTime;
        return delta;
    }

    void Update()
    {
        if (State != MeditationState.Running) return;

        Timer += Time.deltaTime;

        FlowSpeed += GetAccelerationDelta();
        FlowSpeed = Mathf.Clamp(FlowSpeed, 0f, float.MaxValue);

        RhythmSpeed = StartRhythmSpeed + Mathf.Sin(Timer) * RhythmAmplitude;

        bool inRhythmNow = FlowSpeed > RhythmSpeed - RhythmWindow && FlowSpeed < RhythmSpeed + RhythmWindow;
        if (InRhythm && !inRhythmNow) Disruptions++;
        InRhythm = inRhythmNow;

        if (InRhythm) TimeInRhythm += Time.deltaTime;
        if (Timer >= Duration) EndSession();
        MeditationUI.Instance.UpdateLabels();
    }
}