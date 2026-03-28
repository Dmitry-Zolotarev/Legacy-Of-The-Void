using UnityEngine;
using TMPro;
[RequireComponent(typeof(EnemyCombatStats))]
[RequireComponent(typeof(TooltipTrigger))]
public class TravelToFightButton : MonoBehaviour
{
    
    
    [SerializeField] private int RequiredRank = 0;
    [SerializeField] private TextMeshProUGUI RequiredRankLabel;
    [SerializeField] private GameObject nextFightLevel;
    private EnemyCombatStats enemy;
    private TooltipTrigger tooltip;
    public void Start()
    {
        
        enemy = GetComponent<EnemyCombatStats>();
        tooltip = GetComponent<TooltipTrigger>();
        try
        {
            tooltip.tooltipText = "Минимальный ранг: " + GameCore.Instance.Ranks[RequiredRank].Name.ToLower();
            nextFightLevel.SetActive(false);
        }
        catch { } 
    }
    public void Travel()
    {
        if (GameCore.Instance.Master.CurrentRank < RequiredRank) return;
        GameCore.Instance.SelectedEnemy = enemy;
        TravelSystem.Instance.RequiredRank = RequiredRank;
        TravelSystem.Instance.TravelSystemCanvas.SetActive(true);
    }
    public void Update()
    {
        if(enemy != null && enemy.IsDefeated)
        {
            nextFightLevel?.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
