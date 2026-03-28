using UnityEngine;
using TMPro;


public class TravelSystem : MonoBehaviour
{
    [SerializeField] private int MinSilver = 20;
    [SerializeField] private int MaxSilver = 220;
    [SerializeField] private int MinYears = 1;
    [SerializeField] private int MaxYears = 3;
    [SerializeField] private TextMeshProUGUI SilverLabel;
    public GameObject TravelSystemCanvas;
    public static TravelSystem Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void OnEnable()
    {
        TravelSystemCanvas.SetActive(false);
        UpdateLabels();
    }
    public void Travel()
    {
        GameCore.Instance.StartFight();
    }
    private void UpdateLabels()
    {
        SilverLabel.SetText(GameCore.Instance.Master.Silver.ToString()); 
    }
}
