
using System;
using System.Collections.Generic;

[Serializable]
public class CharacterData
{ 
    public enum States {Alive, Injured, Exhausted, Aging, Critical, Dying, Dead, Archived };   
    public int ID;
    public int Generation;
    public int Age;
    public int LifeLimit;
    public string LifeStage;
    public string AliveState;
    public int Body;
    public int Qi;
    public int Spirit;
    public int CurrentQi;
    public int MaxQi;
    public int RankId;
    public int Silver;
    public int Trophies;
    public int Pills;
    public int MeditationProgress;
    public bool BreakthroughReadyFlag;
    public bool DiscipleUnlockedFlag;
    public bool FinalBreakReadyFlag;
    public States currentState;
    public List<int> OpenedMeridians=new List<int>();
    public List<string> KnownTechniques=new List<string>();
    public List<string> EquippedTechniques=new List<string>();
}
