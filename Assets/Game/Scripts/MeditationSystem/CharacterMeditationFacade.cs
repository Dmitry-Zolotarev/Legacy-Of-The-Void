
public class CharacterMeditationFacade
{
    private MeditationResultResolver resolver = new MeditationResultResolver();

    public void ApplyResult(MeditationSessionState session)
    {
        float total = session.timeInRhythm + session.timeOutOfRhythm;
        float ratio = total > 0 ? session.timeInRhythm / total : 0;
        var quality = resolver.Resolve(ratio, session.disruptions);
        var master = GameCore.Instance.Run.CurrentMaster;
        master.Qi += resolver.GetQiReward(quality);
        master.meditationStability += resolver.GetStabilityReward(quality);
    }
}
