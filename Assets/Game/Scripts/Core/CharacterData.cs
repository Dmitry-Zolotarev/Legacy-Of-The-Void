using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData 
{
    private static System.Random random = new System.Random();
    [HideInInspector] public int ID;
    [HideInInspector] public int Generation = 1;
    [HideInInspector] public readonly int LifeLimit;
    [HideInInspector] public int Qi = 0;

    [HideInInspector] public CharacterStates currentState = CharacterStates.Alive;
    [HideInInspector] public bool DiscipleUnlockedFlag = false;
    [HideInInspector] public bool FinalBreakReadyFlag = false;
    
    public int Age = 18;
    public int MinLifeLimit = 60;
    public int MaxLifeLimit = 110;
    public int Body = 10;
    public int Spirit = 10;
    public int CurrentRank = 0;
    public int CurrentMeridian = 0;   
    public int MaxQi = 60;
    public int Silver = 10;
    public int Trophies = 0;
    public int Pills = 0;

    public List<Rank> Ranks = new List<Rank>();
    public List<Meridian> Meridians = new List<Meridian>();  
    public List<string> KnownTechniques = new List<string>();
    public List<string> EquippedTechniques = new List<string>();

    public void OpenMeridian()
    {
        if (CurrentMeridian >= Meridians.Count) return;
        MaxQi = Meridians[CurrentMeridian].MaxQi;
        Spirit = Meridians[CurrentMeridian].Spirit;
        CurrentMeridian++;      
    }

    public CharacterData()
    {
        ID = Guid.NewGuid().GetHashCode();
        LifeLimit = random.Next(MinLifeLimit, MaxLifeLimit);
    }   
}