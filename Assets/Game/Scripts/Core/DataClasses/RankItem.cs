using UnityEngine;

public enum RankItems
{
    SecondFlowPill,
    RedGrass,
    ExternalPill,
    PeakFlower,
}

[System.Serializable]
public class RankItem
{
    public int Count = 0;
    public RankItems ID = RankItems.SecondFlowPill; 
    public string name = "";
}
