using System.Collections.Generic;

[System.Serializable]
public class FightAction : TravelAction
{
    public BattleLaunchData BattleData;
    public int minSilverBonus = 400;
    public int maxSilverBonus = 800;
    public override void DoAction()
    {
        TravelSystem.Instance.SilverBonus = GameCore.Instance.random.Next(minSilverBonus, maxSilverBonus + 1);
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
