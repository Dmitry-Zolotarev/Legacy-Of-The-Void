
using UnityEngine;

public class MeditationController : MonoBehaviour
{
    public MeditationSessionState session;
    public MeditationInputHandler inputHandler;

    public float targetSpeed = 50f;
    public float sessionDuration = 10f;

    private float timer;
    private RhythmEvaluator rhythmEvaluator = new RhythmEvaluator();

    void StartSession(MeditationMode mode)
    {
        session = new MeditationSessionState();
        session.mode = mode;
        session.state = SessionState.Running;
        timer = 0f;
    }

    void Update()
    {
        if (session == null || session.state != SessionState.Running) return;


        timer += Time.deltaTime;

        session.flowSpeed += inputHandler.GetSpeedDelta();

        bool inRhythm = rhythmEvaluator.IsInRhythm(session.flowSpeed, targetSpeed, session.rhythmWindow);

        if (inRhythm)
            session.timeInRhythm += Time.deltaTime;
        else
            session.timeOutOfRhythm += Time.deltaTime;

        if (timer >= sessionDuration) EndSession();

    }

    void EndSession()
    {
        session.state = SessionState.Success;
    }
}
