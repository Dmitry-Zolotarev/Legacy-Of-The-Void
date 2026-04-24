using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FightAction : TravelAction
{
    [SerializeField] private BattleLaunchData BattleData;
    [SerializeField] private int minSilverBonus = 400;
    [SerializeField] private int maxSilverBonus = 800;
    [SerializeField] private RankItem rankItem;

    public override void DoAction()
    {
        TravelSystem.Instance.SilverBonus = GameCore.Instance.random.Next(minSilverBonus, maxSilverBonus + 1);
        if(rankItem.Count > 0) TravelSystem.Instance.LootItem = rankItem;
        LaunchBattle();
        base.DoAction();
    }

    private void LaunchBattle()
    {
        BattleData.SetPlayerData();
        GameCore.Instance.CombatSystem.SetActive(true); 
        MusicPlayer.Instance.PlayCombatMusic();
        AutoBattleController.Instance?.SetupExternalBattle(BattleData);
        GameCore.Instance.MainHub.SetActive(false);
        TravelSystem.Instance.gameObject.SetActive(false);
    }
    public override List<string> GetRewardRows()
    {
        var stringList = new List<string>();
        stringList.Add($"Серебро: {minSilverBonus} - {maxSilverBonus}");
        string itemName = "";
        if (rankItem.Count > 0) itemName = rankItem.name;
        stringList.Add(itemName);
        return stringList;
    }
    public override List<string> GetEffectRows()
    {
        var stringList = new List<string>();
        stringList.Add($"Внутренний демон: +{InternalDemonChange}");
        stringList.Add($"Трата времени: {TimeCostInMonths} мес.");
        return stringList;
    }
}
