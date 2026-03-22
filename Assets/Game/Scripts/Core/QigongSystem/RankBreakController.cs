using UnityEngine;

public class RankBreakController : MonoBehaviour
{
    [SerializeField] private QiOrbController QiOrb;
    [SerializeField] private BreathingController Breathing;
    public static RankBreakController Instance;

    private bool IsRunning = false;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void ToggleSession()
    {
        if (IsRunning) EndSession();
        else StartSession();
    }

    private void StartSession()
    {
        IsRunning = true;
        Breathing.StartBreathing();
        QiOrb.StartMoving();
    }
    public void EndSession()
    {
        IsRunning = false;
        Breathing.StopBreathing();
        QiOrb.StopMoving();
        GameCore.Instance.AdvanceTime(1);
    }
    void Update()
    {
        if (!IsRunning) return;
        if (!Breathing.InRhythm(QiOrb.GetSpeedDelta())) QiOrb.MinusQi(1);
    }
}
