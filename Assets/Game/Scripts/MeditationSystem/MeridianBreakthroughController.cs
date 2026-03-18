
public class MeridianBreakthroughController
{
    public BreakthroughPath chosenPath;
    public float requiredSpeed;
    public float rhythmWindow;

    public BreakthroughOutcome Resolve(float speed, bool inRhythm)
    {
        if (!inRhythm)
            return BreakthroughOutcome.Disruption;

        if (speed >= requiredSpeed)
            return BreakthroughOutcome.CleanSuccess;

        if (speed >= requiredSpeed * 0.9f)
            return BreakthroughOutcome.EdgeSuccess;

        return BreakthroughOutcome.Fail;
    }
}
