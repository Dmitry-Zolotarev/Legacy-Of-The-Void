using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(ParticleSpawner))]
public class MeditationController : MonoBehaviour
{
    [SerializeField] private GameObject StartButton;
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
    [SerializeField] private float RhythmAmplitude = 2f;
    [SerializeField] private float RhythmFrequency = 1f;
    [SerializeField] private float BreathingAmplitude = 0.03f;
    [SerializeField] private float tickTime = 0.5f;

    public static MeditationController Instance;

    private CharacterData master;
    private float QiBonus = 2;
    private float QiGained = 0f;
    private float currentPhase = 0;
    private float giveQiTime;
    private float sessionTime = 0f;
    private ParticleSpawner spawner;
    private float smoothFill = 0;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        spawner = GetComponent<ParticleSpawner>();
    }
    private void OnEnable()
    {
        master = GameCore.Instance.Master;
        giveQiTime = Time.time;
        QiElixirsLabel.SetText(master.QiElixirs.ToString());
        QiLabel.SetText($"Ци: {master.Qi} / {master.MaxQi}");
        UpdateUI();
        QiGained = 0f;
        sessionTime = 0f;
        QiBonus = StartQiBonus;

        if (master.QiElixirs > 0)
        {
            QiBonus = StartQiBonus * ElixirPower;
            master.QiElixirs--;
            spawner.Spawn(QiElixirsLabel.transform, "-1", Color.red);
        }
        smoothFill = master.Qi / master.MaxQi;
    }
    private void UpdateUI()
    {
        QiFluid.fillAmount = (master.Qi + QiGained) / master.MaxQi;

        Dantian.localScale = Vector3.one * (1 + currentPhase * BreathingAmplitude);
    }
    private void Update()
    {
        currentPhase = Mathf.Sin(sessionTime * RhythmFrequency) * RhythmFrequency;

        sessionTime += Time.deltaTime;
        QiGained += QiBonus * Time.deltaTime;
        if(QiGained >= 1)
        {
            master.AddQi((int)QiGained);
            QiLabel.SetText($"Ци: {master.Qi} / {master.MaxQi}");
            QiGained = 0f;    
        }
        UpdateUI();

        if (Time.time > giveQiTime + tickTime)
        {  
            GameCore.Instance.AdvanceTime(1);
            giveQiTime = Time.time;
            QiElixirsLabel.SetText(master.QiElixirs.ToString());
        }
        if (master.Qi == master.MaxQi) ScreenManager.Instance.OpenMenu((int)Canvases.GymCanvas);
    }
}