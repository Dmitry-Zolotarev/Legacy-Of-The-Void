using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public int actionCost = 1;

    public void SpendTime()
    {
        var player = GameManager.Instance.Player;

        player.stats.age += actionCost;

        if (!player.stats.isAlive)
        {
            Debug.Log("Character has died of old age.");
            InheritanceManager.Instance.TriggerInheritance();
        }
    }
}