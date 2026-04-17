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
    [SerializeField] private int InternalDemonIncrease = 1;


    [SerializeField] private float BreathingFrequency = 1f;
    [SerializeField] private float BreathingAmplitude = 0.03f;
    [SerializeField] private float tickTime = 0.5f;
    [SerializeField] private int ElixirPower = 2;
    [SerializeField] private int SpendMonths = 4;
    public static MeditationController Instance;
    private CharacterData master;
    private float QiBonus = 1;
    private float QiGained = 0f;
    private float currentPhase = 0;
    private float giveQiTime;
    private float sessionTime = 0f;
    private ParticleSpawner spawner;

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
        QiBonus = master.InternalDemon.GetCurrentState().QiBonus;

        if (master.QiElixirs > 0)
        {
            QiBonus *= ElixirPower;
            master.QiElixirs--;
            spawner.Spawn(QiElixirsLabel.transform, "-1", Color.red);
        }
    }
    private void UpdateUI()
    {
        QiFluid.fillAmount = (master.Qi + QiGained) / master.MaxQi;

        Dantian.localScale = Vector3.one * (1 + currentPhase * BreathingAmplitude);
    }
    private void Update()
    {
        currentPhase = Mathf.Sin(sessionTime * BreathingFrequency);

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
            GameCore.Instance.AdvanceTime(SpendMonths);
            giveQiTime = Time.time;
            QiElixirsLabel.SetText(master.QiElixirs.ToString());
            master.InternalDemon.Change(InternalDemonIncrease);
        }
        if (master.Qi == master.MaxQi) ScreenManager.Instance.OpenMenu((int)Canvases.GymCanvas);
    }
}