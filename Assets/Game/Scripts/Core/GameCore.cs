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
    [HideInInspector] public Demons SelectedDemon = Demons.NoDemon;    
    [SerializeField] private TextMeshProUGUI GameOverHeader;
    [SerializeField] private TextMeshProUGUI GameOverDescrption;
    [SerializeField] private TextMeshProUGUI AgeLabel;

    [SerializeField] private GameObject ToolTipCanvas;
    [SerializeField] private GameObject GameOverWindow;
    [SerializeField] private GameObject StartHelpCanvas;
    [SerializeField] private GameObject CombatHelpCanvas;
    [SerializeField] private GameObject AgeCanvas;
    [SerializeField] private List<string> SurnameList;
    [SerializeField] private List<string> NameList;
    
    public GameObject ComicsCanvas;
    public List<Rank> Ranks;
    public List<Demon> Enemies;
    public List<Technique> Techniques;
    public List<MeridianLevel> MeridianLevels;
    public GameObject CombatSystem;
    public GameObject MainHub;
    public CharacterData Master;
    public static GameCore Instance;
    [HideInInspector] public System.Random random = new System.Random();
    public bool StartComicShown = false;
    private ParticleSpawner spawner;

    void Awake()
    {
        StartHelpCanvas.SetActive(true);
        if (Instance == null) Instance = this;
        if (SaveManager.NeedLoad) 
        {
            SaveManager.Load(this);
            StartHelpCanvas.SetActive(false);
            CombatHelpCanvas.SetActive(false);
        }   
        spawner = GetComponent<ParticleSpawner>();
    }
    void Start()
    {
        AgeLabel?.SetText(Master.Age.ToString());
        ComicsCanvas?.SetActive(!StartComicShown);
    }
    public string GenerateFullName()
    {
        var random = new System.Random();
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
    
    public string GetRankLabelRu(MasterRank rank)
    {
        try
        {
            return Ranks[(int)rank].Name.ToLower();
        }
        catch { return "основа"; }        
    }
    public void SaveGame()
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
    }
    public void AdvanceTime(int years)
    {
        Year += years;
        Master.Age += years;
        if (Master.Student != null) Master.Student.Age += years;

        if (Master.Age > Master.LifeLimit) KillMaster();

        AgeLabel?.SetText(Master.Age.ToString());
        spawner.Spawn(AgeLabel.transform, $"+{years}", Color.red);
    }
}
