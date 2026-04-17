
[System.Serializable]
public class LootAction : TravelAction
{
    public override void DoAction()
    {
        base.DoAction();
        int silverBonus = GameCore.Instance.random.Next(minSilverBonus, maxSilverBonus + 1);
        GameCore.Instance.Master.Silver += silverBonus;
    }
}
