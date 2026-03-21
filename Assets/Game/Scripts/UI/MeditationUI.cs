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

    private Color defaultCorridorColor;
    private MeditationController meditation; 
    public static MeditationUI Instance;

    [SerializeField] private Sprite MeditationBackground;
    [SerializeField] private Sprite BreakthroughBackground;
    private Image Background;
    void Awake()
    {
        defaultCorridorColor = RhythmCorridor.color;
        Background = GetComponent<Image>();
        if (Instance == null) Instance = this;
    }

    public void Start()
    {
        ResultPanel.SetActive(false);
        MeditationVisualisation.SetActive(false);
        meditation = MeditationController.Instance;
        ToggleBackground();
        ToggleElements();
        UpdateLabels();
    }
    private void ToggleBackground()
    {
        if (meditation.IsBreakthrough())
        {
            Background.sprite = BreakthroughBackground;
        }
        else Background.sprite = MeditationBackground;
    }
    public void ToggleSession()
    {
        meditation.ToggleSession();       
        MeditationVisualisation.SetActive(meditation.State == MeditationState.Running);
        ToggleBackground();
        ToggleElements();
    }
    public void ToggleElements()
    {
        BackButton.SetActive(!meditation.IsBreakthrough() && meditation.State == MeditationState.Idle);
        ToggleSessionButton.SetActive(!meditation.IsBreakthrough() && meditation.State == MeditationState.Idle);
    }
    public void UpdateLabels()
    {
        int secondsLeft = meditation.SecondsLeft();
        TimeLeftLabel.SetText("Осталось времени: " + secondsLeft.ToString());

        if (meditation.IsBreakthrough() && secondsLeft < 1) RhythmCorridor.color = Color.orangeRed;
        else RhythmCorridor.color = defaultCorridorColor;

        if (meditation.InRhythm) PlayerOrb.color = Color.green;
        else PlayerOrb.color = Color.red;

        PlayerOrb.transform.localPosition = new Vector3(meditation.FlowSpeed * VisualisationScale, -66f, 0);
        RhythmCorridor.transform.localPosition = new Vector3(meditation.RhythmSpeed * VisualisationScale, -66f, 0);
        RhythmCorridor.transform.localScale = new Vector3(meditation.RhythmCorridor / 1.5f, 1);
    }
    public void ShowResult()
    {
        if (meditation.Mode == MeditationMode.Normal)
        {
            StartCoroutine(ShowMeditationResult());
        }   
        else if (meditation.IsBreakthrough()) StartCoroutine(ShowBreakthroughResult());
    }
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
        if (master.Qi >= master.MaxQi) ScreenManager.Instance.CloseMenus();
    }
    private IEnumerator ShowBreakthroughResult()
    {
        yield return new WaitForSeconds(showResultsTime / 1.5f);
        if (meditation.InRhythm && meditation.Quality == MeditationQuality.Excellent) CompletedLabel.SetText("Меридиан прорван");
        else CompletedLabel.SetText("Прорыв не удался");

        QiLabel.SetText("");
        MeditationVisualisation.SetActive(false);
        SuccessLabel.SetText($"Попадание в ритм: {(int)(meditation.GetSuccessRatio() * 100)}%");
        ResultPanel.SetActive(true);

        yield return new WaitForSeconds(showResultsTime * 1.5f);
        meditation.Mode = MeditationMode.Normal;
        ResultPanel.SetActive(false);      
        ScreenManager.Instance.OpenMenu(0);
    }
}