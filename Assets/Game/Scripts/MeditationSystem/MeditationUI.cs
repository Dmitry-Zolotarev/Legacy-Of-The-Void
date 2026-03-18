using UnityEngine;
using TMPro;
public class MeditationUI : MonoBehaviour
{
    private MeditationController Master;
    [SerializeField] private TextMeshProUGUI BodyLabel;
    [SerializeField] private TextMeshProUGUI SpiritLabel;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI GenLabel;
    [SerializeField] private TextMeshProUGUI SilverLabel;
    [SerializeField] private TextMeshProUGUI RankLabel;
    [SerializeField] private TextMeshProUGUI AgeLabel;
    [SerializeField] private TextMeshProUGUI StatusLabel;
    void Awake()
    {
    }
    private void Start()
    {
        UpdateLabels();
    }
    public void UpdateLabels()
    {
        
    }
}
