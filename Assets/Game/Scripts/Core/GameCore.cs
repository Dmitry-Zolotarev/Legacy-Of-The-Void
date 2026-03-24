using UnityEngine;
using System.ComponentModel;
using System.Reflection;
using System;
public class GameCore : MonoBehaviour
{  
    
    public static GameCore Instance;
    public CharacterData CurrentMaster;
    [SerializeField] private GameObject GameOverWindow;
    [SerializeField] private GameObject MainHubUI;
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
    public void StartGame() => CurrentMaster = new CharacterData();
    public void AdvanceTime(int years)
    {
        CurrentMaster.Age += years;
        
        if(CurrentMaster.Student != null) CurrentMaster.Student.Age += years;

        if (CurrentMaster.Age > CurrentMaster.LifeLimit)
        {
            ScreenManager.Instance.CloseMenus();

            if (CurrentMaster.Student != null)
            {
                CurrentMaster.Student.Inherit(CurrentMaster);
                CurrentMaster = CurrentMaster.Student;

            }
            else GameOver();
        }      
    }
    private void GameOver()
    {
        MainHubUI.SetActive(false);
        GameOverWindow.SetActive(true);
    }
}
