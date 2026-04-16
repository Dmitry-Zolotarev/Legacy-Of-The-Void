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
    [HideInInspector] public int Year = 1;
    [HideInInspector] public int Month = 0;
    [HideInInspector] public Demons SelectedDemon = Demons.NoDemon;    
    [SerializeField] private TextMeshProUGUI GameOverHeader;
    [SerializeField] private TextMeshProUGUI GameOverDescrption;
    [SerializeField] private TextMeshProUGUI AgeLabel;

    [SerializeField] private GameObject ToolTipCanvas;
    [SerializeField] private GameObject GameOverWindow;
    
    [SerializeField] private GameObject CombatHelpCanvas;
    [SerializeField] private GameObject AgeCanvas;
    [SerializeField] private List<string> SurnameList;
    [SerializeField] private List<string> NameList;
    
    public GameObject ComicsCanvas;
    public List<Rank> Ranks;
    public List<Enemy> Enemies;
    public List<Technique> Techniques;
    public List<MeridianLevel> MeridianLevels;
    public List<InternalDemonState> InternalDemonStates;


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

    void Awake()
    {
        if (Instance == null) Instance = this;
        if (SaveManager.NeedLoad) 
        {
            SaveManager.Load(this);          
        }   
        spawner = GetComponent<ParticleSpawner>();
    }
    void Start()
    {
        AgeLabel?.SetText(Master.Age.ToString());
        ComicsCanvas?.SetActive(!StartComicShown);
        CombatHelpCanvas.SetActive(!CombatHelpShown);
    }
    public string GenerateFullName()
    {
        string surname = SurnameList?[random.Next(0, SurnameList.Count)];
        string name = NameList?[random.Next(0, NameList.Count)];
        return $"{surname} {name}";
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
    private void KillMaster()
    {
        ScreenManager.Instance.CloseMenus();
        GameOverHeader?.SetText($"Мастер {Master.Name} умер");
        
        GameOverWindow.SetActive(true);
        if (Master.CurrentRank >= (int)MasterRank.FirstRate)
        {
            if (Master.Student == null) Master.Student = new Student();
            Master.Student.Inherit(Master);
            Master = Master.Student;
            GameOverDescrption?.SetText("Наследство передано ученику");
        }
        else
        {
            var newGeneration = Master.Generation + 1;
            Master = new CharacterData();
            Master.Generation = newGeneration;
            GameOverDescrption?.SetText("Ученик не подготовлен");
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
    public string GetRankForBecomeTeacher()
    {
        return Ranks[(int)Master.RankForBecomeTeacher].Name;
    }
    public void EndFight()
    {
        MusicPlayer.Instance.PlayMainMusic();
        Instance.MainHub?.SetActive(true);
        Instance.CombatSystem?.SetActive(false);

        var demon = Enemies[(int)SelectedDemon];

        AdvanceTime(1);
        if (SelectedDemon != Demons.NoDemon && demon.IsDead) 
        {           
            ComicsCanvas.SetActive(true);
            demon.SetComicSprite();
        } 
        ScreenManager.Instance.OpenMenu((int)Canvases.MapCanvas);
        CombatHelpShown = true;
    }
    public void AdvanceTime(int months)
    {
        Month += months;      
        int years = Month / 12;
        Month %= 12;

        Year += years;
        Master.Age += years;
        if (Master.Student != null) Master.Student.Age += years;

        if (Master.Age > Master.LifeLimit) KillMaster();

        if(years > 0)
        {
            AgeLabel?.SetText(Master.Age.ToString());
            spawner.Spawn(AgeLabel.transform, $"+{years}", Color.red);
        }       
    }
}