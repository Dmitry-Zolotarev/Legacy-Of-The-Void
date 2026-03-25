using UnityEngine;
using System.ComponentModel;
using System.Reflection;
using System;
using TMPro;
using System.Collections.Generic;

public class GameCore : MonoBehaviour
{  
    
    public static GameCore Instance;
    public CharacterData CurrentMaster;
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
    public string GetYearWord(CharacterData master)
    {
        int age = master.Age;
        var yearWord = "лет";
        if (age % 10 > 1 && age % 10 < 5) yearWord = "года";
        if (age % 10 == 1) yearWord = "год";
        return yearWord;
    }
    private void Start()
    {
        AgeLabel?.SetText(CurrentMaster.Age.ToString());
    }

    public void AdvanceTime(int years)
    {
        CurrentMaster.Age += years;
        if(CurrentMaster.Student != null) CurrentMaster.Student.Age += years;

        AgeLabel?.SetText(CurrentMaster.Age.ToString());

        if (CurrentMaster.Age > CurrentMaster.LifeLimit)
        {
            ScreenManager.Instance.CloseMenus();

            GameOverWindow.SetActive(true);

            GameOverHeader?.SetText($"Мастер {CurrentMaster.Name} умер");

            if (CurrentMaster.Student != null)
            {
                CurrentMaster.Student.Inherit(CurrentMaster);
                CurrentMaster = CurrentMaster.Student;
                GameOverDescrption?.SetText("Наследство передано ученику");
            }
            else
            {
                CurrentMaster = new CharacterData();
                CurrentMaster.Generation++;
                GameOverDescrption?.SetText("Наследство не передано");
            }
        }      
    }
    
}
