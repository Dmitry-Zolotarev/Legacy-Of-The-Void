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
    public int startHp = 50;

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

    // Убираем пустой конструктор или делаем его безопасным
    public BattleLaunchData()
    {
        // Ничего не создаём заново — Unity сам десериализует поля
    }

    public BattleLaunchData(BattleFighterConfig enemyConfig)
    {
        SetPlayerData();
        if (enemyConfig != null)
            Enemy = enemyConfig;           // важно: присваиваем ссылку
        else
            Enemy = new BattleFighterConfig();
    }

    private void SetPlayerData()
    {
        var master = GameCore.Instance.Master;
        if (master == null) master = new CharacterData();

        Player = new BattleFighterConfig();
        Player.bodyLevel = master.Body;
        Player.startQi = master.Qi;
        Player.Rank = (MasterRank)master.CurrentRank;
        Player.dragonFist = master.IsTechniqueUnlocked(TechniqueType.DragonFist);
        Player.craneKick = master.IsTechniqueUnlocked(TechniqueType.CraneKick);
        Player.moonSlash = master.IsTechniqueUnlocked(TechniqueType.MoonSlash);
        Player.voidPalm = master.IsTechniqueUnlocked(TechniqueType.VoidPalm);
    }
    public void PrepareForBattle()
    {
        SetPlayerData();
    }
}
