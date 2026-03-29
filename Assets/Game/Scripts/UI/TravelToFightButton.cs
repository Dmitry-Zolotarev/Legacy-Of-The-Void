using UnityEngine;
using TMPro;
[RequireComponent(typeof(TooltipTrigger))]
public class TravelToFightButton : MonoBehaviour
{

    
    [SerializeField] private GameObject nextFightLevel;
    [SerializeField] private int minSilverBonus = 800;
    [SerializeField] private int maxSilverBonus = 2200;
    [SerializeField] private Sprite levelBackground;
    [SerializeField] private BattleFighterConfig enemy;
    [HideInInspector] public bool enemyDefeated;
    private TooltipTrigger tooltip;
    
    public void Start()
    {
        tooltip = GetComponent<TooltipTrigger>();      
        try
        {
            tooltip.tooltipText += "\nРанг: " + GameCore.Instance.Ranks[(int)enemy.Rank].Name.ToLower();
            nextFightLevel.SetActive(false);
        }
        catch { } 
    }
    public void Travel()
    {
        if (GameCore.Instance.Master.CurrentRank < (int)enemy.Rank)
        {
            tooltip.ShowTooltip("У игрока слишком низкий ранг!");
            return;
        }

        TravelSystem.Instance.SelectedLevel = this;
        TravelSystem.Instance.SilverBonus = GameCore.Instance.random.Next(minSilverBonus, maxSilverBonus + 1);

        BattleLaunchData launchData = new BattleLaunchData(enemy);   // используем конструктор с параметром
        launchData.PrepareForBattle();                               // явно заполняем данные игрока

        TravelSystem.Instance.BattleLaunchButton.BattleData = launchData;

        TravelSystem.Instance.ShowFightDialog(tooltip.tooltipText);

        if (levelBackground != null)
            CombatBackground.Instance.backroundImage.sprite = levelBackground;
    }
}
