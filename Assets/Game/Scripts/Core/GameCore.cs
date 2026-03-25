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
    public static GameCore Instance;
    [SerializeField] private GameObject GameOverWindow;
    [SerializeField] private TextMeshProUGUI GameOverHeader;
    [SerializeField] private TextMeshProUGUI GameOverDescrption;
    [SerializeField] private TextMeshProUGUI AgeLabel;
    void Awake()
    {
        if (Instance != null && Instance.gameObject != gameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
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
    private void Start()
    {
        AgeLabel?.SetText(Master.Age.ToString());
    }

    public void AdvanceTime(int years)
    {
        Year += years;
        Master.Age += years;
        if(Master.Student != null) Master.Student.Age += years;
       
        if (Master.Age > Master.LifeLimit)
        {
            ScreenManager.Instance.CloseMenus();

            GameOverWindow.SetActive(true);

            GameOverHeader?.SetText($"Мастер {Master.Name} умер");

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
        }
        AgeLabel?.SetText(Master.Age.ToString());
    }
    
}
