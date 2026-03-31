using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TooltipTrigger))]
public class TravelToFightButton : MonoBehaviour
{
    [SerializeField] private int minSilverBonus = 800;
    [SerializeField] private int maxSilverBonus = 2200;
    [SerializeField] private Demons demon;
    [SerializeField] private BattleFighterConfig enemy;
    [SerializeField] public string basicToolTipText;
    private TooltipTrigger tooltip;
    
    public void Start()
    {
        tooltip = GetComponent<TooltipTrigger>();

        SetRankToolTIp();
    }
    private void OnEnable()
    {
        if((int)demon < 4) gameObject.SetActive(!GameCore.Instance.Enemies[(int)demon].IsDead);
    }
    private void SetRankToolTIp()
    {
        tooltip.tooltipText = $"{basicToolTipText}\nРанг: " + GameCore.Instance.Ranks[(int)enemy.Rank].Name.ToLower();
    }
    private IEnumerator NotEnoughRank()
    {
        tooltip.ShowTooltip("У игрока слишком низкий ранг!");
        yield return new WaitForSeconds(1.5f);
        tooltip.ShowTooltip();
    }
    public void TravelToFight()
    {
        if (GameCore.Instance.Master.CurrentRank < (int)enemy.Rank)
        {
            StartCoroutine(NotEnoughRank());
            return;
        }
        try
        {
            GameCore.Instance.SelectedDemon = demon;

            BattleLaunchData launchData = new BattleLaunchData(enemy);

            TravelSystem.Instance.EnemyImage.sprite = GameCore.Instance.Enemies[(int)demon].Sprite;
            TravelSystem.Instance.SelectedLevel = this;
            TravelSystem.Instance.SilverBonus = GameCore.Instance.random.Next(minSilverBonus, maxSilverBonus + 1);
            TravelSystem.Instance.BattleLaunchButton.BattleData = launchData;
            TravelSystem.Instance.ShowFightDialog(tooltip.tooltipText);
        }
        catch { }   
    }
}
