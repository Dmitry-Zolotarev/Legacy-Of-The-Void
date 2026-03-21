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
        if (CurrentMaster == null) StartGame();
    }
    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attr = field.GetCustomAttribute<DescriptionAttribute>();
        return attr != null ? attr.Description : value.ToString();
    }
    public void StartGame() 
    {
        CurrentMaster = new CharacterData();
    }
    public void OpenMeridian() => CurrentMaster.OpenMeridian();
    public void AdvanceTime(int years)
    {
        var master = CurrentMaster;
        master.Age += years;

        if (master.Age > CurrentMaster.LifeLimit) 
        {
            master.currentState = CharacterStates.Dead;
            return;
        }
        if (master.Age >= 60) MasterSprite.sprite = OldMasterSprite;
        else if (master.Age >= 40) MasterSprite.sprite = AdultMasterSprite;
        else MasterSprite.sprite = YoungMasterSprite;
    }
    
}
