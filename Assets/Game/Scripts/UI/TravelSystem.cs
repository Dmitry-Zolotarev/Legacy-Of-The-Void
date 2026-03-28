using UnityEngine;
using TMPro;


public class TravelSystem : MonoBehaviour
{
    
    [SerializeField] private int MinYears = 1;
    [SerializeField] private int MaxYears = 3;
    [SerializeField] private TextMeshProUGUI SilverLabel;
    public GameObject TravelSystemCanvas;
    public static TravelSystem Instance;
    [HideInInspector] public int LootedSilver = 0;
    private System.Random random = new System.Random();
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
