using UnityEngine;

[System.Serializable]
public class DantianData
{
    [SerializeField]private int maxQi;
    private int currentQi = 0;

    public void AddQi(int amount)
    {
        currentQi += amount;
        if(currentQi > maxQi) currentQi = maxQi;
    }
    public void SpendQi(int amount)
    {
        currentQi -= amount;
        if (currentQi < 0) currentQi = 0;
    }
    public bool HasEnoughQi(int amount)
    {
        return currentQi >= amount;
    }
}
