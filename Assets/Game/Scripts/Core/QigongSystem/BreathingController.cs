using UnityEngine;

[System.Serializable]
public class BreathingController
{

    public float StartRhythmCorridor = 1.5f;
    [SerializeField] private float RhythmCorridor = 1f;
    [SerializeField] private float RhythmAmplitude = 1f;
    [SerializeField] private float RhythmFrequency = 1f;
    [HideInInspector] public bool IsBreathing = false;

    public float AccelerationDelta = 1f;
    public float MaxDeviation = 5f;
    public bool InRhythm = true;
    private float SessionTime = 0f;

    public void StartBreathing()
    {
        SessionTime = 0f;
        IsBreathing = true;
    }
    public void StopBreathing()
    {
        IsBreathing = false;
        MeditationUI.Instance.ShowResult();  
        GameCore.Instance.AdvanceTime(1);
    }
    public float GetCurrentPhase()
    {
        if (!IsBreathing) return 0f;
        return Mathf.Sin(SessionTime * RhythmFrequency) * RhythmAmplitude;
    }
    public bool IsActionInCorrectWindow(float deltaSpeed)
    {
        return deltaSpeed >= GetCurrentPhase() - RhythmCorridor || deltaSpeed <= GetCurrentPhase() + RhythmCorridor;
    }
}