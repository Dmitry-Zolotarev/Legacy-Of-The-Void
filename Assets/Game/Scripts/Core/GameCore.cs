using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using System.Reflection;
using System;
public class GameCore : MonoBehaviour
{
    [SerializeField] private Sprite YoungMasterSprite, AdultMasterSprite, OldMasterSprite;
    [SerializeField] private Image MasterSprite;
    public static GameCore Instance;
    public CharacterData CurrentMaster;   
    void Awake()
    {      
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attr = field.GetCustomAttribute<DescriptionAttribute>();
        return attr != null ? attr.Description : value.ToString();
    }
    public void StartGame() => CurrentMaster = new CharacterData();

    public void SaveGame() => SaveLoadSystem.Save(CurrentMaster);
    public void LoadGame()
    {
        CurrentMaster = SaveLoadSystem.Load();
        if (CurrentMaster == null) StartGame();
    }
    public void AdvanceTime(int years)
    {
        CurrentMaster.Age += years;
        
        if(CurrentMaster.Student != null) CurrentMaster.Student.Age += years;

        if (CurrentMaster.Age > CurrentMaster.LifeLimit)
        {
            CurrentMaster.Student.Inherit(CurrentMaster);
            CurrentMaster = CurrentMaster.Student;
        }
        if (CurrentMaster.Age >= 60)
        {
            MasterSprite.sprite = OldMasterSprite;
        }
        else if (CurrentMaster.Age >= 40)
        {
            MasterSprite.sprite = AdultMasterSprite;
        } 
        else MasterSprite.sprite = YoungMasterSprite;
    }
    
}
