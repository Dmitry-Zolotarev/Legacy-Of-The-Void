using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData
{
    protected System.Random random = new System.Random();
    [HideInInspector] public int Generation = 1;
    [HideInInspector] public bool DiscipleUnlocked = false;
    [HideInInspector] public bool FinalBreakReadyFlag = false;
    [HideInInspector] public List<Technique> KnownTechniques = new List<Technique>();
    public int Age = 30;
    public int LifeLimit = 70;
    public string Name = "Со Мин";
    public int MinLifeLimit = 65;
    public int MaxLifeLimit = 85;
    public int Body = 5;
    public int MaxBody = 5;
    public int Qi = 0;
    public int MaxQi = 60;
    public int Silver = 20;
    public int OpenedMeridians = 0;
    public int CurrentRank = 0;
    public int BodyElixirs = 0;
    public int QiElixirs = 0;

    public Student Student;
    public InternalDemon InternalDemon = new InternalDemon();
    
    public MasterRank RankForBecomeTeacher = MasterRank.ThirdRate;

    public CharacterData()
    {

    }
    public CharacterData(SaveData data)
    {
        Age = data.Age;
        LifeLimit = data.LifeLimit;
        Name = data.Name;
        Generation = data.Generation;
        MaxBody = data.MaxBody;
        Body = data.Body;
        MaxQi = data.MaxQi;
        OpenedMeridians = data.OpenedMeridians;
        Qi = data.Qi;
        if (data.HasStudent) Student = new Student(data);

        Silver = data.Silver;
        BodyElixirs = data.BodyElixirs;
        QiElixirs = data.QiElixirs;
        CurrentRank = data.CurrentRank;

        foreach(int i in data.KnownTechniques)
        {
            KnownTechniques.Add(GameCore.Instance.Techniques[i]);
        }
    }
    public void OpenMeridian()
    {
        if (OpenedMeridians >= GameCore.Instance.MeridianLevels.Count) return;

        MaxQi = GameCore.Instance.MeridianLevels[OpenedMeridians].MaxQi;
        
        OpenedMeridians++;
    }
    public int TrainBody(int coefficient)
    {
        int startBody = Body;
        var bonus = InternalDemon.GetCurrentState().BodyTrainBonus * coefficient;
        Body += bonus;

        if (Body > MaxBody) Body = MaxBody;
        return Body - startBody;
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
    public void RecoverQi() => Qi = MaxQi;
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
        if (CurrentRank == (int)RankForBecomeTeacher) Student = new Student();
        MaxBody = GameCore.Instance.Ranks[CurrentRank].MaxBody;
    }
    public string GetFullName()
    {
        return $"{Name}, {Age} {GameCore.Instance.GetYearWord(Age)}";
    }
    public string GetRankName(int i)
    {
        return GameCore.Instance.Ranks[i].Name;
    }
    public bool IsTechniqueUnlocked(TechniqueType techniqueType)
    {
        foreach(var technique in KnownTechniques)
        {
            if (technique.Type == (int)techniqueType) return true;
        }
        return false;
    }
}