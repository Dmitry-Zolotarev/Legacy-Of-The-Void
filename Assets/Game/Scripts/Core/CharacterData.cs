using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData
{
    protected System.Random random = new System.Random();
    [HideInInspector] public int Generation = 1;
    [HideInInspector] public readonly int LifeLimit;
    [HideInInspector] public bool DiscipleUnlocked = false;
    [HideInInspector] public bool FinalBreakReadyFlag = false;
    [HideInInspector] public List<Technique> KnownTechniques;

    public int Age = 16;
    public string Name = "";
    public int MinLifeLimit = 65;
    public int MaxLifeLimit = 95;
    public int Body = 10;
    public int CurrentRank = 0;
    public int Qi = 0;
    public int MaxQi = 60;
    public int Silver = 100;
    public int OpenedMeridians = 0;
    
    public int BodyElixirs = 0;
    public int QiElixirs = 0;
    public Student Student;
    public List<Rank> Ranks;
    public List<MeridianLevel> MeridianLevels;
    
    [HideInInspector] public Technique EquippedTechnique;

    public CharacterData()
    {
        Name = GameCore.Instance?.GenerateFullName();
        LifeLimit = random.Next(MinLifeLimit, MaxLifeLimit + 1); 
    }
    public void OpenMeridian()
    {
        if (OpenedMeridians >= MeridianLevels.Count) return;

        MaxQi = MeridianLevels[OpenedMeridians].MaxQi;

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
    public Rank GetNextRank()
    {
        int i = CurrentRank < Ranks.Count - 1 ? CurrentRank + 1 : CurrentRank;   
        return Ranks[i];
    }
    public int GetNextRankID()
    {
        int i = CurrentRank < Ranks.Count - 1 ? CurrentRank + 1 : CurrentRank;
        return i;
    }
    public void UpdateRank()
    {
        if(CurrentRank < Ranks.Count - 1) CurrentRank++;
        if (CurrentRank == 3) 
        {
            Student = new Student();
            Student.MeridianLevels = MeridianLevels;
        }
        
    }
    public string GetStudentName()
    {
        if (Student != null) return Student.Name;
        return "нет";
    }
    public string GetRankName(int i)
    {
        return Ranks[i].Name;
    }
}