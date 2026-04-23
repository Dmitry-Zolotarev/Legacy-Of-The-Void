using System.Collections.Generic;

[System.Serializable]
public class RestAction : TravelAction
{
    public override void DoAction()
    {
        GameCore.Instance.Master.HealWounds();
        TravelSystem.Instance.TravelPanels.SetActive(false);
        TravelSystem.Instance.ModalWindowsCanvas.SetActive(true);
        TravelSystem.Instance.OpenWindow(1);  
        base.DoAction();
    }
    public override List<string> GetRewardRows()
    {
        var stringList = new List<string>();
        stringList.Add($"Внутренний демон: {InternalDemonChange}");
        return stringList;
    }
    public override List<string> GetEffectRows()
    {
        var stringList = new List<string>();
        stringList.Add($"Трата времени: {TimeCostInMonths} мес.");
        return stringList;
    }
}
