using UnityEngine;
using TMPro;

public class RankSystemUI : MonoBehaviour
{
    
    public static RankSystemUI Instance;
    [SerializeField] private TextMeshProUGUI needBodyLabel;
    [SerializeField] private TextMeshProUGUI needMeridiansLabel;
    private CharacterData master;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public void Start()
    {
        master = GameCore.Instance.Run.CurrentMaster;
        UpdateLabels();
    }
    public void UpdateLabels()
    {
        needBodyLabel.SetText($"Тело: {master.Body} / {master.GetNextRank().needBody}");
        needMeridiansLabel.SetText($"Меридианы: {master.OpenedMeridians} / {master.GetNextRank().needMeridians}");
    }
}
