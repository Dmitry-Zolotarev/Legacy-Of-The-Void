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
    [HideInInspector] public List<Technique> KnownTechniques = new List<Technique>();

    public int Age = 16;
    public string Name = "";
    public int MinLifeLimit = 65;
    public int MaxLifeLimit = 95;
    public int Body = 5;
    public int MaxBody = 10;
    public int Qi = 0;
    public int MaxQi = 60;
    public int Silver = 100;
    public int OpenedMeridians = 0;
    public int CurrentRank = 0;
    public int BodyElixirs = 0;
    public int QiElixirs = 0;
    public Student Student;
    public int RankForBecomeTeacher = 3;

    [HideInInspector] public Technique EquippedTechnique;

    public CharacterData()
    {
        Name = GameCore.Instance?.GenerateFullName();
        LifeLimit = random.Next(MinLifeLimit, MaxLifeLimit + 1); 
    }
    public CharacterData(Student student)
    {
        Age = student.Age;
        Name = student.Name;
        LifeLimit = student.LifeLimit;
        OpenedMeridians = student.OpenedMeridians;
        MaxQi = student.MaxQi;
        Qi = student.Qi;
        Silver = student.Silver;
        BodyElixirs = student.BodyElixirs;
        QiElixirs = student.QiElixirs;
        LifeLimit = student.LifeLimit;
    }
    public void OpenMeridian()
    {
        if (OpenedMeridians >= GameCore.Instance.MeridianLevels.Count) return;

        MaxQi = GameCore.Instance.MeridianLevels[OpenedMeridians].MaxQi;
        MaxBody = GameCore.Instance.MeridianLevels[OpenedMeridians].MaxBody;
        OpenedMeridians++;
    }
    public void TrainBody(int amount)
    {
        Body += amount;
        if (Body > MaxBody) Body = MaxBody;
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
        int i = CurrentRank < GameCore.Instance.Ranks.Count - 1 ? CurrentRank + 1 : CurrentRank;   
        return GameCore.Instance.Ranks[i];
    }
    public int GetNextRankID()
    {
        int i = CurrentRank < GameCore.Instance.Ranks.Count - 1 ? CurrentRank + 1 : CurrentRank;
        return i;
    }
    public void UpdateRank()
    {
        if(CurrentRank < GameCore.Instance.Ranks.Count - 1) CurrentRank++;
        if (CurrentRank == RankForBecomeTeacher) Student = new Student();      
    }
    public string GetStudentName()
    {
        if (Student != null) return Student.Name;
        return "нет";
    }
    public string GetRankName(int i)
    {
        return GameCore.Instance.Ranks[i].Name;
    }
}