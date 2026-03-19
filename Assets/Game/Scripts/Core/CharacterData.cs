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
    public int CurrentMeridian = 0;
    public int Rank = 1;
    public int MaxQi = 60;
    public int Silver = 10;
    public int Trophies = 0;
    public int Pills = 0;

    public List<Meridian> meridians;
    public List<string> KnownTechniques = new List<string>();
    public List<string> EquippedTechniques = new List<string>();

    public void OpenMeridian()
    {
        if (CurrentMeridian >= meridians.Count) return;
        MaxQi = meridians[CurrentMeridian].MaxQi;
        Spirit = meridians[CurrentMeridian].Spirit;
        CurrentMeridian++;
        MeridiansUI.Instance.UpdateUI();
        
    }

    public CharacterData()
    {
        ID = Guid.NewGuid().GetHashCode();
        LifeLimit = random.Next(MinLifeLimit, MaxLifeLimit);
        Debug.Log("Master life limit: " + LifeLimit);
    }   
}