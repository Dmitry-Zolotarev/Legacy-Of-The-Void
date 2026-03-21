using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MeditationUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI TimeLeftLabel;   
    [SerializeField] private float VisualisationScale = 30f;

    [SerializeField] private GameObject BackButton;
    [SerializeField] private GameObject ToggleSessionButton;
    [SerializeField] private GameObject MeditationVisualisation;
    [SerializeField] private Image RhythmCorridor;
    [SerializeField] private Image PlayerOrb;

    [SerializeField] private int showResultsTime = 3;

    public GameObject ResultPanel;
    [SerializeField] private TextMeshProUGUI SuccessLabel;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private TextMeshProUGUI CompletedLabel;
    private MeditationController meditation; 
    public static MeditationUI Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void Start()
    {
        ResultPanel.SetActive(false);
        MeditationVisualisation.SetActive(false);
        meditation = MeditationController.Instance;
        ToggleElements();
        UpdateLabels();
    }
    public void ToggleSession()
    {
        meditation.ToggleSession();       
        MeditationVisualisation.SetActive(meditation.State == MeditationState.Running);
        ToggleElements();
    }
    public void ToggleElements()
    {
        BackButton.SetActive(meditation.State == MeditationState.Idle);
        ToggleSessionButton.SetActive(meditation.State == MeditationState.Idle);
    }
    public void UpdateLabels()
    {
        int secondsLeft = meditation.SecondsLeft();
        TimeLeftLabel.SetText("Осталось времени: " + secondsLeft.ToString());

        if (meditation.InRhythm) PlayerOrb.color = Color.green;
        else PlayerOrb.color = Color.red;

        PlayerOrb.transform.localPosition = new Vector3(meditation.FlowSpeed * VisualisationScale, -66f, 0);
        RhythmCorridor.transform.localPosition = new Vector3(meditation.RhythmSpeed * VisualisationScale, -66f, 0);
        RhythmCorridor.transform.localScale = new Vector3(meditation.RhythmCorridor / 1.5f, 1);
    }
    public void ShowResult() => StartCoroutine(ShowMeditationResult());
    private IEnumerator ShowMeditationResult()
    {
        if (meditation.Quality == MeditationQuality.Disrupted)
        {
            CompletedLabel.SetText("Медитация прервана");
        } 
        else CompletedLabel.SetText("Медитация завершена");

        MeditationVisualisation.SetActive(false);
        QiLabel.SetText("Получено ци: " + MeditationController.Instance.GetQiReward());            
        SuccessLabel.SetText($"Попадание в ритм: {(int)(meditation.GetSuccessRatio() * 100)}%");
        ResultPanel.SetActive(true);       

        yield return new WaitForSeconds(showResultsTime);

        ResultPanel.SetActive(false);
        var master = GameCore.Instance.CurrentMaster;
        if (master.Qi >= master.MaxQi) ScreenManager.Instance.OpenMenu(4);
    }
}