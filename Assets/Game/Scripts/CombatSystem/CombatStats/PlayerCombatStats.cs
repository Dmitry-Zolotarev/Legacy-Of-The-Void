using UnityEngine;

public class PlayerCombatStats : FighterCombatStats
{
    private void OnEnable()
    {
        ResetForBattle();
    }

    public override void ResetForBattle()
    {
        var master = GameCore.Instance.Master;
        bodyLevel = master.Body;
        CurrentHP = MaxHP;
        Rank = master.CurrentRank;
        maxQi = master.MaxQi;
        CurrentQi = Mathf.Clamp(master.Qi, 0, MaxQi);
    }
    public override bool IsTechniqueUnlocked(TechniqueType techniqueType)
    {
        return GameCore.Instance.Master.IsTechniqueUnlocked((int)techniqueType);
    }
}