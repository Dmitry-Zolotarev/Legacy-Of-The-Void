using UnityEngine;

[RequireComponent(typeof(EnemyCombatStats))]
public class TravelToFightButton : MonoBehaviour
{
    public GameObject nextFightLevel;
    private EnemyCombatStats enemy;
    public void Start()
    {
        enemy = GetComponent<EnemyCombatStats>();
        nextFightLevel.SetActive(false); 
    }
    public void Travel()
    {
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
