using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public void StartCombat(EnemyData enemy)
    {
        var player = GameManager.Instance.Player;

        int playerPower = CombatCalculator.CalculatePlayerPower(player);

        if (playerPower >= enemy.power)
            Win(enemy);
        else
            Lose();
    }

    void Win(EnemyData enemy)
    {
        GameManager.Instance.Player.silver += enemy.rewardSilver;
        Debug.Log("Victory");
    }

    void Lose()
    {
        Debug.Log("Defeat");
    }
}