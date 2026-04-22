
[System.Serializable]
public class LootAction : TravelAction
{
    public override void DoAction()
    {
        base.DoAction();
        TravelSystem.Instance.SilverBonus = GameCore.Instance.random.Next(minSilverBonus, maxSilverBonus + 1);
        TravelSystem.Instance.TravelPanels.SetActive(false);
        TravelSystem.Instance.AddSilverToPlayer();
        TravelSystem.Instance.ShowLootDialog();     
    }
}
