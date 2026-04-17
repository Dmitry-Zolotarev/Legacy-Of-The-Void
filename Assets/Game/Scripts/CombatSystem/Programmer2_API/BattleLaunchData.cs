using System;
using UnityEngine;


[Serializable]
public class BattleLaunchData
{
    [HideInInspector]
    public BattleFighterConfig Player = new BattleFighterConfig();

    public BattleFighterConfig Enemy = new BattleFighterConfig();
    public void SetPlayerData()
    {
        var master = GameCore.Instance.Master;
        if (master == null) master = new CharacterData();
        Player = new BattleFighterConfig();
        Player.Rank = (MasterRank)master.CurrentRank;
        Player.useManualStats = true;
        Player.overrideStartQi = true;
        Player.startQi = master.Qi;
        Player.qiLevel = master.MaxQi / 5 - 4;
        Player.bodyLevel = master.Body;

        Player.dragonFist = GameCore.Instance.Master.IsTechniqueUnlocked(TechniqueType.DragonFist);
        Player.craneKick = GameCore.Instance.Master.IsTechniqueUnlocked(TechniqueType.CraneKick);
        Player.moonSlash = GameCore.Instance.Master.IsTechniqueUnlocked(TechniqueType.MoonSlash);
        Player.voidPalm = GameCore.Instance.Master.IsTechniqueUnlocked(TechniqueType.VoidPalm);
    }
    public BattleLaunchData(BattleFighterConfig enemyConfig)
    {
        SetPlayerData();
        if (enemyConfig != null) Enemy = enemyConfig;
    }
}
