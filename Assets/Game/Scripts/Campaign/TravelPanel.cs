using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TravelPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Header;
    [SerializeField] private Image Icon;
    [SerializeField] private TextMeshProUGUI Effects;
    [SerializeField] private TextMeshProUGUI Rewards;
    private TravelAction Action;

    public void UpdateAction(TravelAction action)
    {     
        if (action != null)
        {
            Header.SetText(action.HeaderText);
            if(action.Icon != null) Icon.sprite = action.Icon;
            Effects.SetText(action.GetEffectsDescription());
            Rewards.SetText(action.GetRewardsDescription());
            Action = action;
        } 
    }
    public void DoAction() => Action.DoAction();
}
