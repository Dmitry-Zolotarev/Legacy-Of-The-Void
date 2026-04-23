using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootAction : TravelAction
{
    [SerializeField] private RankItems rankItem;
    public override void DoAction()
    {
        GameCore.Instance.RankItems[(int)rankItem].count++;
        TravelSystem.Instance.TravelPanels.SetActive(false);
        TravelSystem.Instance.ShowLootDialog();
        TravelSystem.Instance.LootLabel.SetText(GameCore.Instance.RankItems[(int)rankItem].name);
        base.DoAction();
    }
    public override List<string> GetRewardRows()
    {
        var stringList = new List<string>();
        stringList.Add(GameCore.Instance.RankItems[(int)rankItem].name);
        return stringList;
    }
    public override List<string> GetEffectRows()
    {
        var stringList = new List<string>();
        stringList.Add($"Трата времени: {TimeCostInMonths} мес.");
        return stringList;
    }
}
