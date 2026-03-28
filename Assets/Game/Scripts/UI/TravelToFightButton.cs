using UnityEngine;
using TMPro;
[RequireComponent(typeof(EnemyCombatStats))]
[RequireComponent(typeof(TooltipTrigger))]
public class TravelToFightButton : MonoBehaviour
{
    
    
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
            tooltip.tooltipText = "Минимальный ранг: " + GameCore.Instance.Ranks[enemy.Rank].Name.ToLower();
            nextFightLevel.SetActive(false);
        }
        catch { } 
    }
    public void Travel()
    {
        if (GameCore.Instance.Master.CurrentRank < enemy.Rank) return;
        GameCore.Instance.SelectedEnemy = enemy;
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
