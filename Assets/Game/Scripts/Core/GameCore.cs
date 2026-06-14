using UnityEngine;
using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Reflection;
using System;
using TMPro;

[RequireComponent(typeof(ParticleSpawner))]
public class GameCore : MonoBehaviour
{
    [HideInInspector] public float PlayTime = 0f;
    [HideInInspector] public int Year = 1;
    [HideInInspector] public int Month = 0;
    [HideInInspector] public int CurrentStage = 0;
    [SerializeField] private TextMeshProUGUI GameOverHeader;
    [SerializeField] private TextMeshProUGUI GameOverDescrption;
    [SerializeField] private TextMeshProUGUI AgeLabel;

    [SerializeField] private GameObject ToolTipCanvas;
    [SerializeField] private GameObject GameOverWindow;
    
    [SerializeField] private GameObject CombatHelpCanvas;
    [SerializeField] private GameObject AgeCanvas;
    
    public GameObject ComicsCanvas;
    public List<Rank> Ranks;
    public List<Technique> Techniques;
    public List<MeridianLevel> MeridianLevels;
    public List<InternalDemonState> InternalDemonStates;
    public List<RankItem> RankItems;

    public GameObject CombatSystem;
    public GameObject StartHelpCanvas;
    public GameObject MainHub;
    public CharacterData Master;
    public static GameCore Instance;
    [HideInInspector] public System.Random random = new System.Random();
    public bool StartComicShown = false;
    public bool StartHelpShown = false;
    public bool CombatHelpShown = false;
    private ParticleSpawner spawner;

    private void Awake()
    {
        CombatSystem?.SetActive(false);
        spawner = GetComponent<ParticleSpawner>();

        if (Instance == null) Instance = this;
        if (SaveManager.NeedLoad) 
        {
            SaveManager.Load(this);          
        }
        
    }
    private void Start()
    {
        AgeLabel?.SetText(Master.Age.ToString());
        ComicsCanvas?.SetActive(!StartComicShown);
        CombatHelpCanvas.SetActive(!CombatHelpShown);
    }
    private void Update()
    {
        PlayTime += Time.deltaTime;
    }
    public RankItem GetRankItem()
    {
        return RankItems[(int)Ranks[Master.GetNextRankID()].RequiredItem];
    }
    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attr = field.GetCustomAttribute<DescriptionAttribute>();
        return attr != null ? attr.Description : value.ToString();
    }
    public string GetYearWord(int years)
    {
        var yearWord = "лет";
        if(years > 4 && years < 21) return yearWord;

        if (years % 10 > 1 && years % 10 < 5) yearWord = "года";
        if (years % 10 == 1) yearWord = "год";
        return yearWord;
    }
    public void EndFight()
    {
        MusicPlayer.Instance.PlayMainMusic();
        MainHub?.SetActive(true);
        CombatSystem?.SetActive(false);
        CombatHelpShown = true;
        if (!GameOverWindow.activeSelf) ScreenManager.Instance.OpenMenu((int)Canvases.TravelCanvas);
    }
    public void KillMaster()
    {
        ScreenManager.Instance.CloseMenus();
        GameOverHeader?.SetText($"Мастер {Master.Name} умер");
        
        GameOverWindow.SetActive(true);
        if (Master.Student != null)
        {
            Master.Student.Inherit(Master);
            Master = Master.Student;
            GameOverDescrption?.SetText("Наследство передано ученику");
        }
        else
        {
            GameOverDescrption?.SetText("Линия школы Хуашань прервана");
            MainHubUI.Instance.gameObject.SetActive(false);
            AgeCanvas.SetActive(false);
            ToolTipCanvas.SetActive(false);
        }
        MainHubUI.Instance.RefreshUI();
    } 
    public void SaveAndExit()
    {
        SaveManager.Save(this);
        SceneManager.LoadScene(0);
    }   
    public void AdvanceTime(int months)
    {
        Month += months;      
        int years = Month / 12;
        Month %= 12;

        Year += years;
        Master.Age += years;
        if (Master.Student != null) Master.Student.Age += years;

        if (Master.Age >= Master.LifeLimit) KillMaster();

        if(years > 0)
        {
            AgeLabel?.SetText(Master.Age.ToString());
            spawner.Spawn(AgeLabel.transform, $"+{years}", Color.red);
        }       
    }
}