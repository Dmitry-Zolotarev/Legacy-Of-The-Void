using System;
using UnityEngine;

[Serializable]
public class BattleFighterConfig
{
    public MasterRank Rank = MasterRank.ThirdRate;

    [Header("Manual Stats")]
    public bool useManualStats = false;
    public int bodyLevel = 0;
    public int qiLevel = 0;

    [Header("Optional Start Overrides")]
    public bool overrideStartHp = false;
    public int startHp = 100;

    public bool overrideStartQi = false;
    public int startQi = 20;

    [Header("Available Techniques")]
    public bool dragonFist = true;
    public bool craneKick = true;
    public bool moonSlash = true;
    public bool voidPalm = true;

}

[Serializable]
public class BattleLaunchData
{
    [HideInInspector]
    public BattleFighterConfig Player = new BattleFighterConfig();

    public BattleFighterConfig Enemy = new BattleFighterConfig();
    private void SetPlayerData()
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
