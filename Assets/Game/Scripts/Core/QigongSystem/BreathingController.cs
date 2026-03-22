using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Image))]
public class BreathingController : MonoBehaviour
{
    [SerializeField] private float RhythmCorridor = 1f;
    [SerializeField] private float RhythmAmplitude = 1f;
    [SerializeField] private float RhythmFrequency = 1f;
    [SerializeField] private float DeltaRadius = 0.1f;
    [HideInInspector] public bool  IsBreathing = false;
    [HideInInspector] public float SessionTime = 0f;
    [HideInInspector] public float CurrentPhase = 0;
    
    private Vector2 DefaultBreathRingSize;
    public float MaxDeviation = 5f;
    
    private Image BreatheRingImage;
    private bool inRhythm;
    
    private void Start()
    {
        DefaultBreathRingSize = transform.localScale;
        BreatheRingImage = GetComponent<Image>();
    }
    private void Update()
    {
        if (!IsBreathing) return;
        CurrentPhase = Mathf.Sin(SessionTime * RhythmFrequency) * RhythmAmplitude; 
        SessionTime += Time.deltaTime;
        transform.localScale = DefaultBreathRingSize + Mathf.Sin(SessionTime * RhythmFrequency) * new Vector2(DeltaRadius, DeltaRadius);
        if(inRhythm) BreatheRingImage.color = Color.white;
        else BreatheRingImage.color = Color.orangeRed;
    }
    public void StartBreathing()
    {
        SessionTime = 0f;
        IsBreathing = true;
    }
    public int GetSecondsRounded()
    {
        return (int)Mathf.Round(SessionTime);
    }
    public int GetSeconds()
    {
        return (int)SessionTime;
    }
    public void StopBreathing()
    {
        IsBreathing = false;
        GameCore.Instance.AdvanceTime(1);
    }
    public bool InRhythm(float deltaSpeed)
    {
        if (!IsBreathing) return false;
        inRhythm = deltaSpeed >= CurrentPhase - RhythmCorridor && deltaSpeed <= CurrentPhase + RhythmCorridor;     
        return inRhythm;
    }
}