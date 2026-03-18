[System.Serializable]
public class MeditationSession
{
    public MeditationMode mode;
    public SessionState state;
    public SessionQuality quality;

    public float FlowSpeed = 1f;
    public float RhythmSpeed = 1f;
    public float RhythmTolerance = 1f;
    public float RhytmAccelerationDelta = 0.01f;
    public float Duration = 10f;
    public float timeInRhythm = 0f;
    public int disruptions = 0;

    public bool IsInRhythm()
    {
        return FlowSpeed > RhythmSpeed - RhythmTolerance && FlowSpeed < RhythmSpeed + RhythmTolerance;
    }
    public void End()
    {
        state = SessionState.Success;
        var meditationQuality = SessionQuality.Excellent;
        
        if (disruptions > 3) meditationQuality = SessionQuality.Disrupted;
        else if (timeInRhythm / Duration <= 0.4f) meditationQuality = SessionQuality.Bad;
        else if (timeInRhythm / Duration <= 0.7f) meditationQuality = SessionQuality.Normal;

        GameCore.Instance.Run.CurrentMaster.Qi += GetQiReward(meditationQuality);
        GameCore.Instance.Run.CurrentMaster.meditationStability += GetStabilityReward(meditationQuality);
    }
    private int GetQiReward(SessionQuality quality)
    {
        switch (quality)
        {
            case SessionQuality.Bad: return 1;
            case SessionQuality.Normal: return 3;
            case SessionQuality.Excellent: return 5;
            case SessionQuality.Disrupted: return 0;
        }
        return 0;
    }
    private int GetStabilityReward(SessionQuality quality)
    {
        switch (quality)
        {
            case SessionQuality.Normal: return 1;
            case SessionQuality.Excellent: return 2;
            case SessionQuality.Disrupted: return -1;
        }
        return 0;
    }
}
