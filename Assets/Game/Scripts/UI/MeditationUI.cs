using UnityEngine;
using TMPro;

public class MeditationUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ToggleSessionButtonText;
    [SerializeField] private TextMeshProUGUI FlowSpeedLabel;
    [SerializeField] private TextMeshProUGUI RhythmSpeedLabel;
    [SerializeField] private TextMeshProUGUI TimeLeftLabel;

    private MeditationController meditation;
    public static MeditationUI Instance;
    void Awake() 
    {
        meditation = MeditationController.Instance;
        if (Instance == null) Instance = this;
    } 
    private void Start() => UpdateLabels();

    public void ToggleSession()
    {
        meditation.ToggleSession();

        if (meditation.State == MeditationState.Running)
        {
            ToggleSessionButtonText?.SetText("Прервать медитацию");
        }
        else ToggleSessionButtonText?.SetText("Начать медитацию");
    }
    public void UpdateLabels()
    {
        FlowSpeedLabel.SetText("Скорость потока: " + meditation.FlowSpeed.ToString("F3"));
        RhythmSpeedLabel.SetText("Скорость ритма: " + meditation.RhythmSpeed.ToString("F3"));
        TimeLeftLabel.SetText("Время: " + meditation.SecondsLeft());
    }  
}
