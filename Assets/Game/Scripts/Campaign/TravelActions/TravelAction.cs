using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class TravelAction
{
    public string HeaderText;
    public Sprite Icon;
    public int InternalDemonChange;
    public int TimeCostInMonths;

    public virtual void DoAction()
    {
        GameCore.Instance.Master.InternalDemon.Change(InternalDemonChange);
        GameCore.Instance.AdvanceTime(TimeCostInMonths);
    }
    public virtual List<string> GetRewardRows()
    {
        return new List<string>();
    }
    public virtual List<string> GetEffectRows()
    {
        return new List<string>();
    }
}
