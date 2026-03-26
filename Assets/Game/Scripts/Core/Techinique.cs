using UnityEngine;

[System.Serializable]
public class Techinique
{
    public int ID = 0;
    public string Name = "";
    public bool Unlocked = false;
    public int RequiredRank = 0;

    public void Unlock(CharacterData master)
    {
        if (master.CurrentRank >= RequiredRank) Unlocked = true;
    }
}
