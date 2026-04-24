using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FightAction : TravelAction
{
    [SerializeField] private int minSilverBonus = 400;
    [SerializeField] private int maxSilverBonus = 800;
    [SerializeField] private RankItem rankItem;
    [SerializeField] private bool isFinalBoss;
    [SerializeField] private BattleLaunchData BattleData;


    public override void DoAction()
    {
        TravelSystem.Instance.SilverBonus = GameCore.Instance.random.Next(minSilverBonus, maxSilverBonus + 1);
        if(rankItem.Unlocked) TravelSystem.Instance.LootItem = rankItem;
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

        if(isFinalBoss)
        {
            stringList.Add("Прохождение игры");
        }
        else stringList.Add($"Серебро: {minSilverBonus} - {maxSilverBonus}");

        string itemName = "";
        if (rankItem.Unlocked) itemName = rankItem.name;
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
