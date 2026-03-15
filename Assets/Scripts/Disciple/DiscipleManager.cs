using UnityEngine;

public class DiscipleManager : MonoBehaviour
{
    public static DiscipleManager Instance;

    public DiscipleData disciple;

    void Awake()
    {
        Instance = this;
    }

    public void CreateDisciple()
    {
        disciple = new DiscipleData();
    }

    public void TransferQi(int amount)
    {
        GameManager.Instance.Player.SpendQi(amount);
        disciple.storedQi += amount;
    }
}