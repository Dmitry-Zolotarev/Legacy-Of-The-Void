
using System.Collections;
using TMPro;
using UnityEngine;

public class TravelSystem : MonoBehaviour
{
    [SerializeField] private int MinSilver = 20;
    [SerializeField] private int MaxSilver = 220;
    [SerializeField] private int MinYears = 1;
    [SerializeField] private int MaxYears = 3;
    [SerializeField] private TextMeshProUGUI SilverLabel;
    [SerializeField] private TextMeshProUGUI TimeSpentLabel;
    [SerializeField] private TextMeshProUGUI LootedSilverLabel;
    [SerializeField] private GameObject TravelResultsWindow;
    public void OnEnable()
    {
        UpdateLabels();
    }
    public void Travel()
    {
        int looterSilver = Random.Range(MinSilver, MaxSilver + 1);
        int timeSpent = Random.Range(MinYears, MaxYears + 1);

        GameCore.Instance.Master.Silver += looterSilver;
        GameCore.Instance.AdvanceTime(timeSpent);

        TimeSpentLabel?.SetText($"Затрачено времени: {timeSpent} {GameCore.Instance.GetYearWord(timeSpent)}");
        LootedSilverLabel?.SetText(looterSilver.ToString());
        TravelResultsWindow.SetActive(true);
        UpdateLabels();
    }
    private void UpdateLabels()
    {
        
        SilverLabel.SetText(GameCore.Instance.Master.Silver.ToString()); 
    }
}
