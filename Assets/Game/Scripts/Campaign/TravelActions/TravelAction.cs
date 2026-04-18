using UnityEngine;

[System.Serializable]
public class TravelAction
{
    public string HeaderText;
    public Sprite Icon;
    public int InternalDemonChange;
    public int TimeCostInMonths;

    public int minSilverBonus = 400;
    public int maxSilverBonus = 800;
    public virtual void DoAction()
    {
        if(!(this is FightAction)) TravelSystem.Instance.UpdateStage();
        GameCore.Instance.Master.InternalDemon.Change(InternalDemonChange);
        GameCore.Instance.AdvanceTime(TimeCostInMonths);
    }
    public virtual string GetEffectsDescription()
    {
        return $"\nВремя: +{TimeCostInMonths} мес, демон: {InternalDemonChange}";
    }
    public virtual string GetRewardsDescription()
    {
        return $"\nСеребро: {minSilverBonus} - {maxSilverBonus}";
    }
}
