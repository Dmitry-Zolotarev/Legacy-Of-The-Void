
using TMPro;
using UnityEngine;

public class TravelSystem : MonoBehaviour
{
    [SerializeField] private int MinSilver = 5;
    [SerializeField] private int MaxSilver = 20;
    [SerializeField] private int MinTrophies = 1;
    [SerializeField] private int MaxTrophies = 3;
    [SerializeField] private TextMeshProUGUI SilverLabel;
    public void Travel()
    {
        GameCore.Instance.CurrentMaster.Silver += Random.Range(MinSilver, MaxSilver);
        GameCore.Instance.AdvanceTime(1);
    }
    private void FixedUpdate()
    {
        SilverLabel.SetText(GameCore.Instance.CurrentMaster.Silver.ToString());
    }
}
