using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Image))]
public class BreathingController : MonoBehaviour
{
    public float RhythmCorridor = 1f;
    [SerializeField] private float RhythmAmplitude = 1f;
    [SerializeField] private float RhythmFrequency = 1f;
    [SerializeField] private float DeltaRadius = 0.1f;
    [HideInInspector] public float SessionTime = 0f;
    [HideInInspector] public float CurrentPhase = 0;
    
    private Vector2 DefaultBreathRingSize;
    public float MaxDeviation = 5f;
    
    private Image BreatheRingImage;
    private bool inRhythm;
    private MeditationController meditation;
    private void Start()
    {
        DefaultBreathRingSize = transform.localScale;
        BreatheRingImage = GetComponent<Image>();
        meditation = MeditationController.Instance;
    }
    private void Update()
    {
        CurrentPhase = Mathf.Sin(SessionTime * RhythmFrequency) * RhythmAmplitude; 
        SessionTime += Time.deltaTime;
        transform.localScale = DefaultBreathRingSize + Mathf.Sin(SessionTime * RhythmFrequency) * new Vector2(DeltaRadius, DeltaRadius);

        if(inRhythm || !meditation.IsRunning) BreatheRingImage.color = Color.white;
        else BreatheRingImage.color = Color.orangeRed;
    }
    public void StartBreathing()
    {
        SessionTime = 0f;
    }
    public int GetSecondsRounded()
    {
        return (int)Mathf.Round(SessionTime);
    }
    public int GetSeconds()
    {
        return (int)SessionTime;
    }
    
    public bool InRhythm(float deltaSpeed)
    {
        inRhythm = deltaSpeed >= CurrentPhase - RhythmCorridor && deltaSpeed <= CurrentPhase + RhythmCorridor;     
        return inRhythm;
    }
    public int SlowOrFast(float deltaSpeed)
    {
        if (deltaSpeed < CurrentPhase - RhythmCorridor) return -1;
        if (deltaSpeed > CurrentPhase + RhythmCorridor) return 1;
        inRhythm = deltaSpeed >= CurrentPhase - RhythmCorridor && deltaSpeed <= CurrentPhase + RhythmCorridor;
        return 0;
    }
}