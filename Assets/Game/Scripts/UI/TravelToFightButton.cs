using UnityEngine;

[RequireComponent(typeof(EnemyCombatStats))]
public class TravelToFightButton : MonoBehaviour
{
    public void Travel()
    {
        GameCore.Instance.SelectedEnemy = GetComponent<EnemyCombatStats>();
        TravelSystem.Instance.TravelSystemCanvas.SetActive(true);
    }
}
