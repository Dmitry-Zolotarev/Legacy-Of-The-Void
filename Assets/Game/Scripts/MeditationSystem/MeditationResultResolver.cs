
public class MeditationResultResolver
{
    public SessionQuality Resolve(float inRatio, int disruptions)
    {
        if (disruptions > 3)
            return SessionQuality.Disrupted;

        if (inRatio <= 0.4f)
            return SessionQuality.Bad;

        if (inRatio <= 0.7f)
            return SessionQuality.Normal;

        return SessionQuality.Excellent;
    }

    public int GetQiReward(SessionQuality quality)
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

    public int GetStabilityReward(SessionQuality quality)
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
