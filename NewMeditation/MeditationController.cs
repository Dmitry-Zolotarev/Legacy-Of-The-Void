using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(ParticleSpawner))]
public class MeditationController : MonoBehaviour
{
    [Header("External Controllers")]
    [SerializeField] private QiOrbController QiOrb;
    [SerializeField] private QiOrbController BreatheOrb;
    [SerializeField] private Image BreatheOrbSprite;
    [Header("UI")]
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject BackButton;
    [SerializeField] private TextMeshProUGUI QiLabel;
    [SerializeField] private Image QiFluid;
    [SerializeField] private TextMeshProUGUI TimeLeftLabel;
    [SerializeField] private TextMeshProUGUI QiElixirsLabel;
    [SerializeField] private GameObject QiElixirsPanel;   
    [SerializeField] private GameObject AgeLabel;
    [SerializeField] private Transform Dantian;

    [SerializeField] private int StartQiBonus = 2;
    [SerializeField] private int ElixirPower = 2;
    [SerializeField] private float Duration = 15f;
    public float RhythmCorridor = 1f;
    [SerializeField] private float RhythmAmplitude = 2f;
    [SerializeField] private float RhythmFrequency = 1f;
    [SerializeField] private float BreathingAmplitude = 0.03f;

    private bool isRunning = false, elixirUsed;
    public static MeditationController Instance;

    private CharacterData master;
    private float QiBonus = 2;
    private float QiGained = 0f;
    private float SessionTime = 0f;
    private float CurrentPhase = 0;
    private ParticleSpawner spawner;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        spawner = GetComponent<ParticleSpawner>();
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
            elixirUsed = true;
        }

        ToggleElements(true);
        
        SessionTime = 0f;
    }

    private void EndSession()
    {
        QiElixirsLabel.SetText(master.QiElixirs.ToString());
        master.AddQi((int)QiGained);
        if(SessionTime > Duration / 2f) GameCore.Instance.AdvanceTime(1);

        ToggleElements(false);
        QiGained = 0f;

        if(elixirUsed)
        {
            spawner.Spawn(QiElixirsLabel.transform, "-1", Color.red);
            elixirUsed = false;
        }  
    }
    private void ToggleElements(bool value)
    {
        isRunning = value;

        QiOrb?.gameObject.SetActive(value);
        BreatheOrb?.gameObject.SetActive(value);
        TimeLeftLabel?.gameObject.SetActive(value);

        Cursor.visible = !value;
        QiElixirsPanel?.SetActive(!value);
        AgeLabel?.SetActive(!value);
        BackButton?.SetActive(!value);
        QiLabel.gameObject.SetActive(!value);

        StartButton?.SetActive(!value && master.Qi < master.MaxQi);

        if (!isRunning)
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

    bool IsOverlap(RectTransform a, RectTransform b)
    {
        Rect rectA = new Rect(a.position, a.rect.size);
        Rect rectB = new Rect(b.position, b.rect.size);
        return rectA.Overlaps(rectB);
    }

    private void Update()
    {
        if (!isRunning) return;
        CurrentPhase = Mathf.Sin(SessionTime * RhythmFrequency) * RhythmAmplitude;

        BreatheOrb.CurrentSpeed = BreatheOrb.StartSpeed + CurrentPhase;

        SessionTime += Time.deltaTime;

        Color breatheOrbColor;

        if (IsOverlap(QiOrb.GetComponent<RectTransform>(), BreatheOrb.GetComponent<RectTransform>()))
        {
            QiGained += QiBonus * Time.deltaTime;
            breatheOrbColor = Color.green;         
        }    
        else breatheOrbColor = Color.orangeRed;

        breatheOrbColor.a = 0.5f;
        BreatheOrbSprite.color = breatheOrbColor;

        if (SessionTime >= Duration || (int)(master.Qi + QiGained) >= master.MaxQi) EndSession();
        UpdateUI();
    }
}