using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int Year;

    public int Age;
    public string Name;
    public int Generation;
    public int LifeLimit;

    public int Body;
    public int MaxBody;
    public int Qi;
    public int MaxQi;

    public int Silver;
    public int BodyElixirs;
    public int QiElixirs;
    public int InternalDemonValue;
    public int Wounds;

    public int OpenedMeridians;
    public int CurrentRank;
    public int CurrentStage;

    public bool StartComicShown;
    public bool StartHelpShown;
    public bool CombatHelpShown;
    public bool HasStudent;

    public string StudentName;
    public int StudentAge;
    public int StudentLifeLimit;

    public int StudentQi;
    public int StudentMaxQi;
    public int StudentOpenedMeridians;
    public List<int> KnownTechniques = new List<int>();
    public List<int> RankItemCounts = new List<int>();
}