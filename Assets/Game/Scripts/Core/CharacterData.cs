using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData 
{
    public enum States { Alive, Injured, Exhausted, Aging, Critical, Dying, Dead, Archived };
    private static System.Random random = new System.Random();
    [HideInInspector] public int ID;
    [HideInInspector] public int Generation = 1;
    [HideInInspector] public readonly int LifeLimit;
    [HideInInspector] public int Qi = 0;

    [HideInInspector] public States currentState = States.Alive;
    [HideInInspector] public bool DiscipleUnlockedFlag = false;
    [HideInInspector] public bool FinalBreakReadyFlag = false;

    public int Age = 18;
    public int MinLifeLimit = 60;
    public int MaxLifeLimit = 110;
    public int Body = 50;
    public int Spirit = 50;
    public int Rank = 1;
    public int MaxQi = 100; 
    
    public int Silver = 10;
    public int Trophies = 0;
    public int Pills = 0;
    
    public List<int> OpenedMeridians = new List<int>();
    public List<string> KnownTechniques = new List<string>();
    public List<string> EquippedTechniques = new List<string>();

    public CharacterData()
    {
        ID = Guid.NewGuid().GetHashCode();
        LifeLimit = random.Next(MinLifeLimit, MaxLifeLimit);
        Debug.Log("Master life limit: " + LifeLimit);
    }
}