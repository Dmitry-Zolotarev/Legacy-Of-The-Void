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
    public RunData Run;   
    void Awake()
    {      
        if (Instance == null) Instance = this;
        if (Run == null) StartGame();
    }
    public static string GetEnumDescription(Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attr = field.GetCustomAttribute<DescriptionAttribute>();
        return attr != null ? attr.Description : value.ToString();
    }
    public void StartGame()
    {
        Run = new RunData();
        Run.RunId = System.Guid.NewGuid().ToString();
        Run.GenerationIndex = 1;
        Run.RunState = "HubActive";
        Run.CurrentMaster = new CharacterData();
    }
    public void AdvanceTime(int years)
    {   
        var master = Run.CurrentMaster;
        master.Age += years;

        if (master.Age > Run.CurrentMaster.LifeLimit) 
        {
            master.currentState = CharacterStates.Dead;
            return;
        }
        if (master.Age >= 60) MasterSprite.sprite = OldMasterSprite;
        else if (master.Age >= 40) MasterSprite.sprite = AdultMasterSprite;
        else MasterSprite.sprite = YoungMasterSprite;
    }
    public void OpenMeridian() => Run.CurrentMaster.OpenMeridian();
}
