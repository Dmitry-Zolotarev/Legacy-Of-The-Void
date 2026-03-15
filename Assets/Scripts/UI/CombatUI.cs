using UnityEngine;

public class CombatUI : MonoBehaviour
{
    public CombatManager combatManager;

    public void AttackEnemy(EnemyData enemy)
    {
        combatManager.StartCombat(enemy);
    }
}