using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MeditationUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ToggleSessionButtonText;
    [SerializeField] private TextMeshProUGUI TimeLeftLabel;
    [SerializeField] private TextMeshProUGUI SuccessLabel;
    [SerializeField] private TextMeshProUGUI QiLabel;

    [SerializeField] private float VisulisationScale = 30f;
    [SerializeField] private GameObject BackButton;
    [SerializeField] private GameObject MeditationVisualisation;
    [SerializeField] private Transform MeditationBar;
    [SerializeField] private Transform RhythmCorridor;
    [SerializeField] private Image PlayerOrb;

    [SerializeField] private int showResultsTime = 3;
    public GameObject ResultPanel;
    private MeditationController meditation;
    public static MeditationUI Instance;


    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        ToggleMeditationVisualisation(false);
        meditation = MeditationController.Instance;
        MeditationBar.transform.localScale = new Vector3(meditation.MaxDeviation, 1f);
        UpdateLabels();
    }

    private void ToggleMeditationVisualisation(bool value)
    {
        TimeLeftLabel.gameObject.SetActive(value);
        MeditationVisualisation.SetActive(value);
    }
    public void ToggleSession() => StartCoroutine(ToggleSessionCoroutine());
    private IEnumerator ToggleSessionCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        meditation.ToggleSession();
        ToggleElements();
    }
    public void ToggleElements()
    {
        if (meditation.State == MeditationState.Running)
        {
            ToggleSessionButtonText.SetText("Прервать медитацию");
        }
        else ToggleSessionButtonText.SetText("Начать медитацию");

        BackButton.SetActive(meditation.State == MeditationState.Idle);
        ToggleMeditationVisualisation(meditation.State == MeditationState.Running);
    }
    public void UpdateLabels()
    {
        TimeLeftLabel.SetText(meditation.SecondsLeft().ToString());

        if (meditation.InRhythm) PlayerOrb.color = Color.green;
        else PlayerOrb.color = Color.red;

        PlayerOrb.transform.localPosition = new Vector3(meditation.FlowSpeed * VisulisationScale, 0, 0);
        RhythmCorridor.localPosition = new Vector3(meditation.RhythmSpeed * VisulisationScale, 0, 0);
        RhythmCorridor.localScale = new Vector3(meditation.RhythmWindow / 1.5f, 1);
    }

    public IEnumerator ShowMeditationResult()
    {
        ResultPanel.SetActive(true);
        ToggleMeditationVisualisation(false);     

        int successPercent = (int)(MeditationController.Instance.GetSuccessRatio() * 100);
        SuccessLabel.SetText($"Попадание в ритм: {successPercent}%");
        QiLabel.SetText("Получено ци: " + MeditationController.Instance.GetQiReward());

        yield return new WaitForSeconds(showResultsTime);
        ResultPanel.SetActive(false);
    }
}