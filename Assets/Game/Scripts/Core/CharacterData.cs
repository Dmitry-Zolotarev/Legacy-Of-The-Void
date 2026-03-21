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
    [HideInInspector] public bool DiscipleUnlocked = false;
    [HideInInspector] public bool FinalBreakReadyFlag = false;
    [HideInInspector] public int OpenedMeridians = 0;

    public int Age = 18;
    public string Name = "";
    public int MinLifeLimit = 60;
    public int MaxLifeLimit = 99;
    public int Body = 10;
    public int Spirit = 10;
    public int CurrentRank = 0;
    
    public int MaxQi = 60;
    public int Silver = 10;
    public int Trophies = 0;
    public int Pills = 0;

    private Student student;
    public List<Rank> Ranks = new List<Rank>();
    public List<Meridian> Meridians = new List<Meridian>();  
    public List<string> KnownTechniques = new List<string>();
    public List<string> EquippedTechniques = new List<string>();

    public void OpenMeridian()
    {
        if (OpenedMeridians >= Meridians.Count) return;
        MaxQi = Meridians[OpenedMeridians].MaxQi;
        Spirit = Meridians[OpenedMeridians].Spirit;
        OpenedMeridians++;      
    }

    public CharacterData()
    {
        ID = Guid.NewGuid().GetHashCode();
        LifeLimit = random.Next(MinLifeLimit, MaxLifeLimit);
    }   
    public Rank GetNextRank()
    {
        int i = CurrentRank < Ranks.Count - 1 ? CurrentRank + 1 : CurrentRank;
        return Ranks[i];
    }
    public string GetStudentName()
    {
        if (student != null) return student.Name;
        return "нет";
    }
}