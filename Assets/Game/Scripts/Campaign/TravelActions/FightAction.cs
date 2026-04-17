
[System.Serializable]
public class FightAction : TravelAction
{
    public BattleLaunchData BattleData;

    public override void DoAction()
    {
        base.DoAction();
        TravelSystem.Instance.SilverBonus = GameCore.Instance.random.Next(minSilverBonus, maxSilverBonus + 1);
        LaunchBattle();       
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
}
