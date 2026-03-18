
public class RhythmEvaluator
{
    public bool IsInRhythm(float speed, float target, float window)
    {
        return speed >= target - window && speed <= target + window;
    }
}
