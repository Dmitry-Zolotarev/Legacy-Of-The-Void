
using UnityEngine;

public class MeditationController : MonoBehaviour
{
    public MeditationSession meditationSession;
    public MeditationInputHandler inputHandler;
    

    private float timer;
    public static MeditationController Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void StartSession(int mode)
    {
        meditationSession = new MeditationSession();
        meditationSession.mode = (MeditationMode)mode;
        meditationSession.state = SessionState.Running;
        timer = 0f;
    }
    public void EndSession() => meditationSession.End();
    void Update()
    {
        if (meditationSession == null || meditationSession.state != SessionState.Running) return;


        timer += Time.deltaTime;

        meditationSession.FlowSpeed += inputHandler.GetSpeedDelta();
        meditationSession.RhythmSpeed += meditationSession.RhytmAccelerationDelta;

        if (meditationSession.IsInRhythm())
        {
            meditationSession.timeInRhythm += Time.deltaTime;
        }   
        if (timer >= meditationSession.Duration) EndSession();
    }   
}
