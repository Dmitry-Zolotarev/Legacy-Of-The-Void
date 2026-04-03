using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
[RequireComponent(typeof(Transform))]
public class MeditationController : MonoBehaviour
{
    [Header("External Controllers")]
    [SerializeField] private QiOrbController QiOrb;

    [Header("UI")]
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject BackButton;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private Image QiFluid;
    [SerializeField] private TextMeshProUGUI TimeLeftLabel;
    [SerializeField] private TextMeshProUGUI QiElixirsLabel;
    [SerializeField] private GameObject QiElixirsPanel;
    [SerializeField] private GameObject MouseButtonsHint;    
    [SerializeField] private GameObject AgeLabel;
    [SerializeField] private Transform Dantian;

    [SerializeField] private int StartQiBonus = 2;
    [SerializeField] private int ElixirPower = 2;
    [SerializeField] private float Duration = 30f;
    public float RhythmCorridor = 1f;
    [SerializeField] private float RhythmAmplitude = 2f;
    [SerializeField] private float RhythmFrequency = 1f;
    [SerializeField] private float BreathingAmplitude = 0.03f;

    [HideInInspector] public bool IsRunning = false;
    public static MeditationController Instance;

    private CharacterData master;
    private float QiBonus = 2;
    private float QiGained = 0f;
    private float SessionTime = 0f;
    private float CurrentPhase = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void OnEnable()
    {
        master = GameCore.Instance.Master;

        QiElixirsLabel.SetText(master.QiElixirs.ToString());
        UpdateUI();
        ToggleElements(false);
    }
    public void StartSession()
    {
        QiGained = 0f;
        QiBonus = StartQiBonus;

        if (master.QiElixirs > 0)
        {
            QiBonus = StartQiBonus * ElixirPower;
            master.QiElixirs--;
        }

        ToggleElements(true);
        MouseButtonsHint.SetActive(false);
        SessionTime = 0f;
    }

    private void EndSession()
    {
        QiElixirsLabel.SetText(master.QiElixirs.ToString());
        master.AddQi((int)QiGained);
        GameCore.Instance.AdvanceTime(1);

        ToggleElements(false);
        QiGained = 0f;
    }

    private void ToggleElements(bool value)
    {
        IsRunning = value;

        QiOrb?.gameObject.SetActive(value);
        TimeLeftLabel?.gameObject.SetActive(value);

        Cursor.visible = !value;
        QiElixirsPanel?.SetActive(!value);
        AgeLabel?.SetActive(!value);
        BackButton?.SetActive(!value);

        StartButton?.SetActive(!value && master.Qi < master.MaxQi);

        if (!IsRunning)
        {
            QiLabel.SetText($"Ци: {master.Qi} / {master.MaxQi}");
            QiLabel.color = Color.white;
        }
    }

    private int SecondsLeft()
    {
        return (int)Mathf.Round(Duration - SessionTime);
    }

    private void UpdateUI()
    {
        TimeLeftLabel?.SetText(SecondsLeft().ToString());
        float currentQi = master.Qi + QiGained;
        QiFluid.fillAmount = currentQi / master.MaxQi;
        Dantian.localScale = Vector3.one * (1 + CurrentPhase * BreathingAmplitude);
    }

    private void SetRhythmLabelText()
    {
        int state = SlowOrFast(QiOrb.GetSpeedDelta());
        
        if (state < 0)
        {
            QiLabel.SetText("Быстрее ↑");
            QiLabel.color = Color.orangeRed;
        }
        else if (state > 0)
        {
            QiLabel.SetText("Медленнее ↓");
            QiLabel.color = Color.orange;
        }
        else
        {
            QiLabel.SetText("В ритме");
            QiLabel.color = Color.green;
        }
    }

    private bool InRhythm(float deltaSpeed)
    {
        return deltaSpeed >= CurrentPhase - RhythmCorridor && deltaSpeed <= CurrentPhase + RhythmCorridor;
    }

    private int SlowOrFast(float deltaSpeed)
    {
        if (deltaSpeed < CurrentPhase - RhythmCorridor) return -1;
        if (deltaSpeed > CurrentPhase + RhythmCorridor) return 1;
        return 0;
    }

    private void Update()
    {
        

        if (!IsRunning) return;
        CurrentPhase = Mathf.Sin(SessionTime * RhythmFrequency) * RhythmAmplitude;
        SessionTime += Time.deltaTime;
        SetRhythmLabelText();

        if (InRhythm(QiOrb.GetSpeedDelta()))
        {
            QiGained += QiBonus * Time.deltaTime;
        }    
        if (SessionTime >= Duration || (int)(master.Qi + QiGained) >= master.MaxQi) EndSession();
        UpdateUI();
    }
}