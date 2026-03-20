using System.ComponentModel;
public enum MeditationMode { Normal, StableBreakthrough, RiskyBreakthrough }
public enum MeditationState { Idle, Running }
public enum MeditationQuality { Bad, Normal, Excellent, Disrupted }
public enum BreakthroughPath { Stable, Risky }
public enum BreakthroughOutcome { CleanSuccess, EdgeSuccess, Fail, Disruption }

public enum CharacterStates
{
    [Description("жив")]
    Alive,
    [Description("ранен")]
    Injured,
    [Description("мёртв")]
    Dead
}