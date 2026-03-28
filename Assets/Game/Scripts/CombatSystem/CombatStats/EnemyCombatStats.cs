using UnityEngine;

public class EnemyCombatStats : FighterCombatStats
{
    public int MinSilver = 800;
    public int MaxSilver = 2200;
    [HideInInspector] public bool IsDefeated = false;
    [HideInInspector] public int lootedSilver = 0;

    public void AddSilverToPlayer()
    {
        lootedSilver = GameCore.Instance.random.Next(MinSilver, MaxSilver + 1);
        GameCore.Instance.Master.Silver += lootedSilver;
        TravelSystem.Instance.LootedSilver = lootedSilver;
    }
}
