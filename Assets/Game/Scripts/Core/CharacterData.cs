using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum CharacterStates
{
    [Description("жив")]
    Alive,
    [Description("ранен")]
    Injured,
    [Description("мёртв")]
    Dead
}
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
    public int Silver = 100;
    public int Trophies = 0;
    public int Pills = 0;

    private Student student;
    public List<Rank> Ranks = new List<Rank>();
    public List<MeridianLevel> MeridiansLevels = new List<MeridianLevel>();
    public List<string> KnownTechniques = new List<string>();
    public List<string> EquippedTechniques = new List<string>();
    public CharacterData()
    {
        ID = Guid.NewGuid().GetHashCode();
        LifeLimit = random.Next(MinLifeLimit, MaxLifeLimit);
    }
    public void OpenMeridian()
    {
        if (OpenedMeridians >= MeridiansLevels.Count) return;

        MaxQi = MeridiansLevels[OpenedMeridians].MaxQi;
        Spirit = MeridiansLevels[OpenedMeridians].Spirit;

        OpenedMeridians++;
    }
    public void AddQi(int amount)
    {
        Qi += amount;
        if (Qi > MaxQi) Qi = MaxQi;
    }
    public void SpendQi(int amount)
    {
        if (Qi >= amount) Qi -= amount;
        
    }
    public string GetStudentName()
    {
        if (student != null) return student.Name;
        return "нет";
    }
}