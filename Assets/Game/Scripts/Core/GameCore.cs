using UnityEngine;
using System.ComponentModel;
using System.Reflection;
using System;
using TMPro;
using System.Collections.Generic;

public class GameCore : MonoBehaviour
{
    [HideInInspector] public int Year = 1;
    public CharacterData Master;
    
    [SerializeField] private GameObject GameOverWindow;
    [SerializeField] private TextMeshProUGUI GameOverHeader;
    [SerializeField] private TextMeshProUGUI GameOverDescrption;
    [SerializeField] private TextMeshProUGUI AgeLabel;
    public List<Technique> Techniques;
    [SerializeField] private List<string> SurnameList;
    [SerializeField] private List<string> NameList;
    public static GameCore Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        if (Techniques.Count > 0)
        {
            Master.KnownTechniques.Add(Instance.Techniques[0]);
            Master.EquippedTechnique = Instance.Techniques[0];
        }
        AgeLabel?.SetText(Master.Age.ToString());
    }
    public string GenerateFullName()
    {
        var random = new System.Random();
        var surname = "";
        surname = SurnameList?[random.Next(0, SurnameList.Count)];
        var name = "";
        name = NameList?[random.Next(0, NameList.Count)];
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
        if (years % 10 > 1 && years % 10 < 5) yearWord = "года";
        if (years % 10 == 1) yearWord = "год";
        return yearWord;
    }   
    public void AdvanceTime(int years)
    {
        Year += years;
        Master.Age += years;
        if(Master.Student != null) Master.Student.Age += years;
       
        if (Master.Age > Master.LifeLimit)
        {
            var oldMaster = Master;
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
                Master = new CharacterData();
                Master.Generation++;
                GameOverDescrption?.SetText("Наследство не передано");
            }
            MainHubUI.Instance.RefreshUI();
        }
        AgeLabel?.SetText(Master.Age.ToString());
    }
    
}
