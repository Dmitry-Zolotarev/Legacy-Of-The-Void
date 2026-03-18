
public static class MeditationResultResolver
{
    public static SessionQuality Resolve(float timeInRhithm, float totalTime, int disruptions)
    {
        if (disruptions > 3) return SessionQuality.Disrupted;

        float meditationQuality = timeInRhithm / totalTime;

        if (meditationQuality <= 0.4f) return SessionQuality.Bad;
        if (meditationQuality <= 0.7f) return SessionQuality.Normal;
        return SessionQuality.Excellent;
    }

    
    
}
