using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class TravelPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Header;
    [SerializeField] private Image Icon;
    [SerializeField] private List<TextMeshProUGUI> RewardLabels;
    [SerializeField] private List<TextMeshProUGUI> EffectLabels;
    private TravelAction Action;

    public void UpdateAction(TravelAction action)
    {     
        if (action != null)
        {
            Header.SetText(action.HeaderText);
            if(action.Icon != null) Icon.sprite = action.Icon;
            Action = action;
        }
        var effects = action.GetEffectRows();
        var rewards = action.GetRewardRows();

        if(effects.Count == EffectLabels.Count)
        {
            for (int i = 0; i < effects.Count; i++) EffectLabels[i].SetText(effects[i]);
        }
        if (rewards.Count == RewardLabels.Count)
        {
            for (int i = 0; i < rewards.Count; i++) RewardLabels[i].SetText(rewards[i]);
        }
    }
    public void DoAction() => Action.DoAction();
}
