using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainHubUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SilverAmountLabel;

    [SerializeField] private Image MasterSprite;
    [SerializeField] private Sprite YoungMasterSprite;
    [SerializeField] private Sprite AdultMasterSprite;
    [SerializeField] private Sprite OldMasterSprite;
    [SerializeField] private StatsPanel MiniStatsPanel;
    public static MainHubUI Instance;
    private CharacterData master;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start()
    {
        RefreshUI();                   
    }
    public void RefreshUI()
    {
        if (GameCore.Instance == null || GameCore.Instance.Master == null) return;
        master = GameCore.Instance.Master;  

        UpdateLabels();
        UpdateMasterSprite();
    }
    public void UpdateMasterSprite()
    {
        if (master == null || MasterSprite == null) return;
        if (master.Age >= 60)
        {
            MasterSprite.sprite = OldMasterSprite;
        }
        else if (master.Age >= 40)
        {
            MasterSprite.sprite = AdultMasterSprite;
        }
        else MasterSprite.sprite = YoungMasterSprite;
    }
    private void UpdateLabels()
    {
        if (master == null) return;
        MiniStatsPanel.UpdateLabels();
        SilverAmountLabel?.SetText(master.Silver.ToString());
    }
}