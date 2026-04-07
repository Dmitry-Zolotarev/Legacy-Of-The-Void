using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TooltipTrigger))]
public class TravelToFightButton : MonoBehaviour
{
    [SerializeField] private int minSilverBonus = 800;
    [SerializeField] private int maxSilverBonus = 2200;
    
    [SerializeField] private BattleFighterConfig enemy;
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private Demons demon;

    private TooltipTrigger tooltip;
    
    public void Awake()
    {
        tooltip = GetComponent<TooltipTrigger>();
        tooltip.tooltipText = $"{tooltip.tooltipText}\nРанг: " + GameCore.Instance.Ranks[(int)enemy.Rank].Name.ToLower();
    }
    private void OnEnable()
    {
        if((int)demon < 4) gameObject.SetActive(!GameCore.Instance.Enemies[(int)demon].IsDead);
    }
    public void TravelToFight()
    {
        GameCore.Instance.SelectedDemon = demon;
        BattleLaunchData launchData = new BattleLaunchData(enemy);
        TravelSystem.Instance.EnemyImage.sprite = enemySprite;
        TravelSystem.Instance.SelectedLevel = this;
        TravelSystem.Instance.SilverBonus = GameCore.Instance.random.Next(minSilverBonus, maxSilverBonus + 1);
        TravelSystem.Instance.BattleLaunchButton.BattleData = launchData;
        TravelSystem.Instance.ShowFightDialog(tooltip.tooltipText);
    }
}
