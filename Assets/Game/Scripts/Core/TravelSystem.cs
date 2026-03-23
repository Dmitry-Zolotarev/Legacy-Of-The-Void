
using UnityEngine;

public class TravelSystem : MonoBehaviour
{
    [SerializeField] private int MinSilver = 5;
    [SerializeField] private int MaxSilver = 20;
    [SerializeField] private int MinTrophies = 1;
    [SerializeField] private int MaxTrophies = 3;
    public void Travel()
    {
        var master = GameCore.Instance.CurrentMaster;

        if (master.healthState == CharacterStates.Normal)
        {
            master.Silver += Random.Range(MinSilver, MaxSilver);
            master.Trophies += Random.Range(MinTrophies, MaxTrophies);
            GameCore.Instance.AdvanceTime(1);
        }
    }
}
