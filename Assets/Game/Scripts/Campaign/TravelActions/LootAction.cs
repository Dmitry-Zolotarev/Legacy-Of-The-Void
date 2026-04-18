
[System.Serializable]
public class LootAction : TravelAction
{
    public override void DoAction()
    {
        TravelSystem.Instance.SilverBonus = GameCore.Instance.random.Next(minSilverBonus, maxSilverBonus + 1);
        TravelSystem.Instance.AddSilverToPlayer();
        base.DoAction();
    }
}
