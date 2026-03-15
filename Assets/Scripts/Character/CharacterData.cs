using System.Collections.Generic;

[System.Serializable]
public class CharacterData
{
    public CharacterStats stats;
    public List<int> openedMeridians = new List<int>();

    public int silver;

    public void AddQi(int value)
    {
        stats.qi += value;
    }

    public void SpendQi(int value)
    {
        stats.qi -= value;
        if (stats.qi < 0)
            stats.qi = 0;
    }
}