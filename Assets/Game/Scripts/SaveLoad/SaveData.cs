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
    public int Silver;

    public int OpenedMeridians;
    public int CurrentRank;
    public bool StartComicShown;

    public List<bool> DemonStates = new List<bool>();
}